DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_UserPassword`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_UserPassword`(IN `p_userId` BINARY(16),
                                                          IN `p_userPassword` BINARY(64),
                                                          IN `p_userSalt` BINARY(40)) MODIFIES SQL DATA
  SQL SECURITY INVOKER
BEGIN


  UPDATE tblStudenti tS
  SET `bPassword` = `p_userPassword`,
      `bSalt`     = `p_userSalt`
  WHERE tS.pk_tblStudenti = `p_userId`;


END$$


DELIMITER ;