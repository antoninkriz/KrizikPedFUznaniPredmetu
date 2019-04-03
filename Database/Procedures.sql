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
DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_UserByEmail`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_UserByEmail`(IN `p_userEmail` VARCHAR(255)) READS SQL DATA
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
DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_UserByGuid`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_UserByGuid`(IN `p_userId` BINARY(16)) READS SQL DATA
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
  WHERE tSt.`pk_tblStudenti` = `p_userId`;


END$$


DELIMITER ;
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
DELIMITER $$


DROP PROCEDURE IF EXISTS `sp_SearchObory`$$
CREATE
  DEFINER =`root`@`localhost` PROCEDURE `sp_SearchObory`(IN `p_search` VARCHAR(64),
                                                         IN `p_katedraId` BINARY(16),
                                                         IN `p_druhStudiaId` BINARY(16)) READS SQL DATA
  SQL SECURITY INVOKER
BEGIN


  SELECT tOb.`pk_tblObory`  AS 'id',
         tOb.`sKod`         AS 'sKod',
         tOb.`sNazev`       AS 'sNazev',
         tOb.`sPoznamka`    AS 'sPoznamka',
         tOb.`nRokZacatek`  AS 'nRokZacatek',
         tOb.`nRokUkonceni` AS 'nRokUkonceni',
         tOb.`bFormaStudia` AS 'bFormaStudia'
  FROM tblPredmetyNaObory tPnO
         INNER JOIN tblPredmety tPr ON tPnO.fk_tblPredmety = tPr.pk_tblPredmety
         INNER JOIN tblObory tOb ON tPnO.fk_tblObory = tOb.pk_tblObory
         INNER JOIN tblKatedry tKa ON tPr.fk_tblKatedra = tKa.pk_tblKatedry
         INNER JOIN tblDruhyStudia tDS on tOb.fk_tblDruhyStudia = tDS.pk_tblDruhyStudia
  WHERE tKa.`pk_tblKatedry` = `p_katedraId`
    AND tDS.`pk_tblDruhyStudia` = `p_druhStudiaId`
    AND CONCAT(tOb.`sKod`, ' ', tDS.`sNazev`) LIKE CONCAT('%', `p_search`, '%')
  GROUP BY tOb.pk_tblObory;


END$$


DELIMITER ;
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
