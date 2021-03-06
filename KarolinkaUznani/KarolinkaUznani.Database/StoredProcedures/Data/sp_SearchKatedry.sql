DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_SearchKatedry`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_SearchKatedry`(IN `p_search` VARCHAR(64)) READS SQL DATA
  SQL SECURITY INVOKER
BEGIN


  SELECT tKa.`pk_tblKatedry` AS 'id',
         tKa.`sKod`          AS 'sKod'
  FROM `tblKatedry` tKa
  WHERE tKa.`sKod` LIKE CONCAT('%', `p_search`, '%');


END$$


DELIMITER ;