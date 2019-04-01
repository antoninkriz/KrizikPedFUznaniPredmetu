DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_UserDelete`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_UserDelete`(IN `p_userId` BINARY(16)) MODIFIES SQL DATA
  SQL SECURITY INVOKER
BEGIN


  DELETE FROM tblStudenti
  WHERE tblStudenti.pk_tblStudenti = `p_userId`;


END$$


DELIMITER ;