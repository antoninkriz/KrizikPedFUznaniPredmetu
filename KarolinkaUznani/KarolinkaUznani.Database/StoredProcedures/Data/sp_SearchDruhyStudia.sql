DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_SearchDruhyStudia`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_SearchDruhyStudia`(IN `p_search` VARCHAR(64),
                                                               IN `p_katedraId` BINARY(16)) READS SQL DATA
  SQL SECURITY INVOKER
BEGIN


  SELECT tDS.`pk_tblDruhyStudia` AS 'id',
         tDS.`sZkratka`          AS 'sZkratka',
         tDS.`sNazev`            AS 'sNazev'
  FROM tblPredmetyNaObory tPnO
         INNER JOIN tblPredmety tPr ON tPnO.fk_tblPredmety = tPr.pk_tblPredmety
         INNER JOIN tblObory tOb ON tPnO.fk_tblObory = tOb.pk_tblObory
         INNER JOIN tblKatedry tKa ON tPr.fk_tblKatedra = tKa.pk_tblKatedry
         INNER JOIN tblDruhyStudia tDS on tOb.fk_tblDruhyStudia = tDS.pk_tblDruhyStudia
  WHERE tKa.`pk_tblKatedry` = `p_katedraId`
    AND CONCAT(tDS.`sZkratka`, ' ', tDS.`sNazev`) LIKE CONCAT('%', `p_search`, '%')
  GROUP BY tDS.`pk_tblDruhyStudia`;


END$$


DELIMITER ;