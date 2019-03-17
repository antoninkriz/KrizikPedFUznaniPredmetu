DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_SearchObory`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_SearchObory`(IN `p_search` VARCHAR(64),
                                                         IN `p_katedraId` BINARY(16),
                                                         IN `p_druhStudiaId` BINARY(16)) READS SQL DATA
  SQL SECURITY INVOKER
BEGIN


  SELECT tOb.`pk_tblObory`  as 'id',
         tOb.`sKod`         as 'sKod',
         tOb.`sNazev`       as 'sNazev',
         tOb.`sPoznamka`    as 'sPoznamka',
         tOb.`nRokZacatek`  as 'nRokZacatek',
         tOb.`nRokUkonceni` as 'nRokUkonceni',
         tOb.`bFormaStudia` as 'bFormaStudia'
  FROM tblPredmetyNaObory tPnO
         INNER JOIN tblPredmety tPr ON tPnO.fk_tblPredmety = tPr.pk_tblPredmety
         INNER JOIN tblObory tOb ON tPnO.fk_tblObory = tOb.pk_tblObory
         INNER JOIN tblKatedry tKa ON tPr.fk_tblKatedra = tKa.pk_tblKatedry
         INNER JOIN tblDruhyStudia tDS on tOb.fk_tblDruhyStudia = tDS.pk_tblDruhyStudia
  WHERE tKa.`pk_tblKatedry` = `p_katedraId`
    AND tDS.`pk_tblDruhyStudia` = `p_druhStudiaId`
    AND CONCAT(tOb.`sKod`, ' ', tDS.`sNazev`) LIKE CONCAT('%', `p_search`, '%')
  GROUP BY tDS.pk_tblDruhyStudia;


END$$


DELIMITER ;