DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_UserAdd`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_UserAdd`(IN `p_userId` BINARY(16),
                                                     IN `p_userCode` INT,
                                                     IN `p_userEmail` VARCHAR(254),
                                                     IN `p_userName` VARCHAR(255),
                                                     IN `p_userSurname` VARCHAR(255),
                                                     IN `p_userPhone` VARCHAR(16),
                                                     IN `p_userPassword` BINARY(64),
                                                     IN `p_userSalt` BINARY(40),
                                                     IN `p_userCreatedAt` DATETIME) MODIFIES SQL DATA
  SQL SECURITY INVOKER
BEGIN


  INSERT INTO tblStudenti (`pk_tblStudenti`,
                           `nCode`,
                           `sEmail`,
                           `sName`,
                           `sSurname`,
                           `sPhone`,
                           `bPassword`,
                           `bSalt`,
                           `dCreatedAt`)
  VALUES (`p_userId`,
          `p_userCode`,
          `p_userEmail`,
          `p_userName`,
          `p_userSurname`,
          `p_userPhone`,
          `p_userPassword`,
          `p_userSalt`,
          `p_userCreatedAt`);


END$$


DELIMITER ;