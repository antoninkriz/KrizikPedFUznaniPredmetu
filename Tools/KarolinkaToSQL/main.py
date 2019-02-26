from uuid import *
from urllib import request
from lxml.html import parse, fromstring
from lxml.etree import tostring

obory = []
predmety = {}
katedry = {}

docHtml = str(request.urlopen('http://studium.pedf.cuni.cz/karolinka/2018/plany.html').read(), 'utf-8').split(
    'CŽV Studium pedagogických věd')[0]
prezencniHtml, kombinovaneHtml = docHtml.split('Kombinované studium', 1)
prezBak, prezencniHtml = prezencniHtml.split('Bakalářské studium', 1)
prezMag, prezNav = prezencniHtml.split('Navazující magisterské studium', 1)
kombBak, kombinovaneHtml = kombinovaneHtml.split('Bakalářské studium', 1)
kombMag, kombNav = kombinovaneHtml.split('Navazující magisterské studium', 1)
druhy = {
    'bak': {
        'data': prezBak + kombBak,
        'nazev': 'Bakalářské',
        'zkratka': 'Bc.',
        'id': uuid4()
    },
    'mag': {
        'data': prezMag + kombMag,
        'nazev': 'Magisterské',
        'zkratka': 'Mgr.',
        'id': uuid4()
    }, 'nav': {
        'data': prezNav + kombNav,
        'nazev': 'Navazující magisterské',
        'zkratka': 'NMgr.',
        'id': uuid4()
    }
}

idObory = 1
for d in druhy:
    doc = fromstring(druhy[d]['data'])
    for table in doc.cssselect('table'):
        for row in table.cssselect('tr:not(:first-child)'):
            tds = row.cssselect('td')

            kod = tds[0].text_content()
            obor = tds[1].text_content()
            specifikace = tds[2].text_content()
            platnostOd = tds[3].text_content()
            platnostDo = tds[4].text_content()

            obory.append({
                'id': uuid4(),
                'druh': druhy[d]['id'],
                'kod': None if kod == '' else kod,
                'obor': None if obor == '' else obor,
                'specifikace': None if specifikace == '' else specifikace,
                'platnostOd': None if platnostOd == '' else platnostOd,
                'platnostDo': None if platnostDo == '' else platnostDo,
                'predmety': []
            })

            idObory += 1

for o in obory:
    doc = parse('http://studium.pedf.cuni.cz/karolinka/2018/' + o['kod'] + '.html').getroot()

    baseDiv = doc.cssselect('body > div')[0]
    baseString = str(tostring(baseDiv), 'utf-8')
    baseString = baseString[baseString.find('<table class="PredmTab pruhy"'):baseString.rfind('</table>')]
    splited = baseString.split('asdf<p class="NadpisSekce2">Povinn&#283; voliteln&#233; p&#345;edm&#283;ty</p>')

    if len(splited) == 1 and len(splited[0]) != 0:
        for row in fromstring(splited[0]).cssselect('table tr:not(:first-child)'):
            if len(row) != 12:
                continue

            tds = row.cssselect('td')

            kod = tds[0].text_content()
            nazev = tds[1].text_content()
            ukonceniTemp = tds[4].text_content()

            ukZ = False
            ukKZ = False
            ukZK = False
            ukKLP = False

            kredity = tds[5].text_content()
            katedra = tds[7].text_content()

            if ukonceniTemp == 'Z':
                ukZ = True
            elif ukonceniTemp == 'KZ':
                ukKZ = True
            elif ukonceniTemp == 'ZK':
                ukZK = True
            elif ukonceniTemp == 'Z+Zk':
                ukZ = True
                ukZK = True
            elif ukonceniTemp == 'KLP':
                ukKLP = True

            o['predmety'].append({
                'kod': kod,
                'nazev': nazev,
                'kredity': kredity,
                'katedra': katedra,
                'ukZ': ukZ,
                'ukKZ': ukKZ,
                'ukZK': ukZK,
                'ukKLP': ukKLP
            })

idPredmety = 1
for o in obory:
    for p in o['predmety']:
        if p['kod'] not in predmety:
            p['id'] = uuid4()
            predmety[p['kod']] = {
                'obory': [o['id']],
                'predmet': p
            }

            idPredmety += 1
        else:
            predmety[p['kod']]['obory'].append(o['id'])

        if p['katedra'] not in katedry:
            uid = uuid4()
            katedry[p['katedra']] = uid
            p['katedra'] = uid
        else:
            uid = katedry[p['katedra']]
            p['katedra'] = uid

out = 'INSERT INTO tblDruhyStudia VALUES '
for d in druhy:
    out += '({}, {}, {}),\n'.format(
        'UNHEX("' + str(druhy[d]['id']).replace('-', '') + '")',
        '"' + druhy[d]['zkratka'] + '"',
        '"' + druhy[d]['nazev'] + '"')
out = out[:-2] + ';\n'

out += 'INSERT INTO tblObory VALUES '
for o in obory:
    out += '({}, UNHEX("{}"), {}, {}, {}, {}, {}),\n'.format('UNHEX("' + str(o['id']).replace('-', '') + '")',
                                                             str(o['druh']).replace('-', ''),
                                                             '"' + o['kod'] + '"',
                                                             '"' + o['obor'].replace('-', '') + '"',
                                                             '"' + o['specifikace'] + '"' if o[
                                                                 'specifikace'] else 'NULL',
                                                             o['platnostOd'] if o['platnostOd'] else 'NULL',
                                                             o['platnostDo'] if o['platnostDo'] else 'NULL')
out = out[:-2] + ';\n'

out += 'INSERT INTO tblPredmety VALUES '
for pr in predmety:
    p = predmety[pr]['predmet']
    out += '({}, {}, {}, {}, {}, {}, {}, {}, {}),\n'.format('UNHEX("' + str(p['id']).replace('-', '') + '")',
                                                            'UNHEX("' + str(p['katedra']).replace('-', '') + '")',
                                                            '"' + p['kod'] + '"',
                                                            '"' + p['nazev'].replace('\n', '') + '"',
                                                            p['kredity'],
                                                            1 if p['ukZ'] else 0,
                                                            1 if p['ukKZ'] else 0,
                                                            1 if p['ukZK'] else 0,
                                                            1 if p['ukKLP'] else 0)
out = out[:-2] + ';\n'

out += "INSERT INTO tblKatedry VALUES "
for k in katedry:
    out += '({}, "{}"),\n'.format('UNHEX("' + str(katedry[k]).replace('-', '') + '")',
                                k)
out = out[:-2] + ';\n'

out += 'INSERT INTO tblPredmetyNaObory VALUES '
for o in obory:
    for pr in o['predmety']:
        out += '(UNHEX("{}"), UNHEX("{}"), UNHEX("{}")),\n'.format(str(uuid4()).replace('-', ''),
                                                                   str(o['id']).replace('-', ''),
                                                                   str(predmety[pr['kod']]['predmet']['id']).replace(
                                                                       '-', ''))
out = out[:-2] + ';\n'

print(out)
