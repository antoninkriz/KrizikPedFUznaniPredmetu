DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_UserUpdate`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_UserUpdate`(IN `p_userId` BINARY(16),
                                                        IN `p_userCode` INT,
                                                        IN `p_userEmail` VARCHAR(254),
                                                        IN `p_userName` VARCHAR(255),
                                                        IN `p_userSurname` VARCHAR(255),
                                                        IN `p_userPhone` VARCHAR(16)) MODIFIES SQL DATA
  SQL SECURITY INVOKER
BEGIN


  UPDATE tblStudenti tS
  SET `nCode`    = `p_userCode`,
      `sEmail`   = `p_userEmail`,
      `sName`    = `p_userName`,
      `sSurname` = `p_userSurname`,
      `sPhone`   = `p_userPhone`
  WHERE tS.pk_tblStudenti = `p_userId`;


END$$


DELIMITER ;