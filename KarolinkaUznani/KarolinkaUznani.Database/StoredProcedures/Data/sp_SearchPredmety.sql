DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_SearchPredmety`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_SearchPredmety`(IN `p_search` VARCHAR(64),
                                                            IN `p_oborId` BINARY(16)) READS SQL DATA
  SQL SECURITY INVOKER
BEGIN


  SELECT tPr.`pk_tblPredmety` as 'id',
         tPr.`sKod`           as 'sKod',
         tPr.`sNazev`         as 'sNazev',
         tPr.`nKredity`       as 'nKredity',
         tPr.`bUkZ`           as 'bUkZ',
         tPr.`bUkKZ`          as 'bUkKZ',
         tPr.`bUkZK`          as 'bUkZK',
         tPr.`bUkKLP`         as 'bUkKLP'
  FROM tblPredmetyNaObory tPnO
         INNER JOIN tblPredmety tPr ON tPnO.fk_tblPredmety = tPr.pk_tblPredmety
  WHERE tPnO.`fk_tblPredmety` = `p_oborId`
    AND CONCAT(tPr.`sKod`, ' ', tPr.`sNazev`) LIKE CONCAT('%', `p_search`, '%')
  GROUP BY tPr.`pk_tblPredmety`;


END$$


DELIMITER ;