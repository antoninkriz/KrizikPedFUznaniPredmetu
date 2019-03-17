DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_SearchPredmety`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_SearchPredmety`(IN `p_search` VARCHAR(64),
                                                            IN `p_oborId` BINARY(16)) READS SQL DATA
  SQL SECURITY INVOKER
BEGIN


  SELECT tPr.`pk_tblPredmety` AS 'id',
         tPr.`sKod`           AS 'sKod',
         tPr.`sNazev`         AS 'sNazev',
         tPr.`nKredity`       AS 'nKredity',
         tPr.`bUkZ`           AS 'bUkZ',
         tPr.`bUkKZ`          AS 'bUkKZ',
         tPr.`bUkZK`          AS 'bUkZK',
         tPr.`bUkKLP`         AS 'bUkKLP'
  FROM tblPredmetyNaObory tPnO
         INNER JOIN tblPredmety tPr ON tPnO.fk_tblPredmety = tPr.pk_tblPredmety
  WHERE tPnO.`fk_tblObory` = `p_oborId`
    AND CONCAT(tPr.`sKod`, ' ', tPr.`sNazev`) LIKE CONCAT('%', `p_search`, '%')
  GROUP BY tPr.`pk_tblPredmety`;


END$$


DELIMITER ;