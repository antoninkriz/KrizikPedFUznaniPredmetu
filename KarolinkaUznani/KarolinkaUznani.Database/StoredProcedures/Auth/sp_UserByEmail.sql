DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_UserByGuid`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_UserByGuid`(IN `p_userEmail` VARCHAR(255)) READS SQL DATA
  SQL SECURITY INVOKER
BEGIN


  SELECT tSt.`pk_tblStudenti` AS 'id',
         tSt.`nCode`          AS 'nCode',
         tSt.`sEmail`         AS 'sEmail',
         tSt.`sName`          AS 'sName',
         tSt.`sSurname`       AS 'sSurname',
         tSt.`sPhone`         AS 'sPhone',
         tSt.`bPassword`      AS 'bPassword',
         tSt.`bSalt`          AS 'bSalt',
         tSt.`dCreatedAt`     AS 'dCreatedAt'
  FROM tblStudenti tSt
  WHERE tSt.`sEmail` = `p_userEmail`;


END$$


DELIMITER ;