-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2.1
-- http://www.phpmyadmin.net
--
-- Počítač: localhost
-- Vytvořeno: Úte 08. led 2019, 11:36
-- Verze serveru: 5.7.24-0ubuntu0.16.04.1
-- Verze PHP: 7.2.13-1+ubuntu16.04.1+deb.sury.org+1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

--
-- Databáze: `Karolinka`
--
CREATE DATABASE IF NOT EXISTS `Karolinka` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_520_ci;
USE `Karolinka`;

DELIMITER $$
--
-- Procedury
--
DROP PROCEDURE IF EXISTS `sp_OborByKod`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_OborByKod` (IN `p_kod` VARCHAR(10))  READS SQL DATA
    SQL SECURITY INVOKER
BEGIN
    

    SELECT
        tOb.`id` as 'id',
        tOb.`sKod` as 'sKod',
        tOb.`sObor` as 'sObor',
        tOb.`sSpecifikace` as 'sSpecifikace',
        tOb.`nPlatnostOd` as 'nPlatnostOd',
        tOb.`nPlatnostDo` as 'nPlatnostDo'
    FROM `tblObory` tOb
    WHERE tOb.`sKod` LIKE CONCAT('%', `p_kod`, '%');

    
  END$$

DROP PROCEDURE IF EXISTS `sp_OborByObor`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_OborByObor` (IN `p_obor` VARCHAR(255))  READS SQL DATA
    SQL SECURITY INVOKER
BEGIN
    

    SELECT
        tOb.`id` as 'id',
        tOb.`sKod` as 'sKod',
        tOb.`sObor` as 'sObor',
        tOb.`sSpecifikace` as 'sSpecifikace',
        tOb.`nPlatnostOd` as 'nPlatnostOd',
        tOb.`nPlatnostDo` as 'nPlatnostDo'
    FROM `tblObory` tOb
    WHERE tOb.`sObor` LIKE CONCAT('%', `p_obor`, '%');

    
  END$$

DROP PROCEDURE IF EXISTS `sp_PredmetyForOboryByKod`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PredmetyForOboryByKod` (IN `p_obor` INT UNSIGNED, IN `p_kod` VARCHAR(10))  READS SQL DATA
    SQL SECURITY INVOKER
SELECT
    tPr.`id` as 'id',
    tPr.`sKod` as 'sKod',
    tPr.`sNazev` as 'sNazev',
    tPr.`nKredity` as 'nKredity',
    tPr.`sKatedra` as 'sKatedra',
    tPr.`bUkZ` as 'bUkZ',
    tPr.`bUkKZ` as 'bUkKZ',
    tPr.`bUkZK` as 'bUkZK',
    tPr.`bUkKLP` as 'bUkKLP'
FROM `tblPredmetyNaObory` tPO
INNER JOIN `tblPredmety` tPr
	ON tPO.`fk_tblPredmety` = tPr.`id`
WHERE
	tPO.`fk_tblObory` = `p_oborId` AND
    tPr.`sKod` LIKE CONCAT('%', `p_kod`,'%')$$

DROP PROCEDURE IF EXISTS `sp_PredmetyForOboryByNazev`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PredmetyForOboryByNazev` (IN `p_obor` INT UNSIGNED, IN `p_nazev` VARCHAR(255))  READS SQL DATA
    SQL SECURITY INVOKER
SELECT
    tPr.`id` as 'id',
    tPr.`sKod` as 'sKod',
    tPr.`sNazev` as 'sNazev',
    tPr.`nKredity` as 'nKredity',
    tPr.`sKatedra` as 'sKatedra',
    tPr.`bUkZ` as 'bUkZ',
    tPr.`bUkKZ` as 'bOkKZ',
    tPr.`bUkZK` as 'bUkZK',
    tPr.`bUkKLP` as 'bUkKLP'
FROM `tblPredmetyNaObory` tPO
INNER JOIN `tblPredmety` tPr
	ON tPO.`fk_tblPredmety` = tPr.`id`
WHERE
	tPO.`fk_tblObory` = `p_oborId` AND
    tPr.`sNazev` LIKE CONCAT('%', `p_nazev`,'%')$$

DROP PROCEDURE IF EXISTS `sp_StudentiAdd`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_StudentiAdd` ()  MODIFIES SQL DATA
    SQL SECURITY INVOKER
BEGIN

	INSERT INTO `tblStudenti` (`nIdOsoby`, `sName`, `sSurname`, `sStreet`, `sCity`, `nZipCode`, `sEmail`, `xHash`) VALUES (`p_idOsoby`, `p_name`, `p_surname`, `p_street`, `p_city`, `p_zipCode`, `p_email`, `p_hash`);
    
END$$

DROP PROCEDURE IF EXISTS `sp_StudentiForEmail`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_StudentiForEmail` (IN `p_email` VARCHAR(255))  READS SQL DATA
    SQL SECURITY INVOKER
BEGIN

    SELECT
    	tSi.`id` as 'id',
    	tSi.`nIdOsoby` as 'nIdOsoby',
        tSi.`sName` as 'sName',
        tSi.`sSurname` as 'sSurname',
        tSi.`sStreet` as 'sStreet',
        tSi.`sCity` as 'sCity',
        tSi.`nZipCode` as 'nZipCode',
        tSi.`sEmail` as 'sEmail',
        tSi.`xHash` as 'xHash'
    FROM `tblStudenti` tSi
    WHERE tSi.`sEmail` = `p_email`;
 
 END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Struktura tabulky `tblDruhyStudia`
--

DROP TABLE IF EXISTS `tblDruhyStudia`;
CREATE TABLE IF NOT EXISTS `tblDruhyStudia` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `sZkratka` varchar(5) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sNazev` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

-- --------------------------------------------------------

--
-- Struktura tabulky `tblObory`
--

DROP TABLE IF EXISTS `tblObory`;
CREATE TABLE IF NOT EXISTS `tblObory` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `fk_tblDruhyStudia` int(10) UNSIGNED NOT NULL,
  `sKod` varchar(10) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sObor` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sSpecifikace` varchar(255) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `nPlatnostOd` int(11) DEFAULT NULL,
  `nPlatnostDo` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `kod` (`sKod`),
  KEY `fk_tblDruhyStudia` (`fk_tblDruhyStudia`)
) ENGINE=InnoDB AUTO_INCREMENT=255 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci ROW_FORMAT=COMPACT;

-- --------------------------------------------------------

--
-- Struktura tabulky `tblPredmety`
--

DROP TABLE IF EXISTS `tblPredmety`;
CREATE TABLE IF NOT EXISTS `tblPredmety` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `sKod` varchar(10) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sNazev` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `nKredity` int(11) UNSIGNED NOT NULL,
  `sKatedra` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `bUkZ` bit(1) NOT NULL DEFAULT b'0',
  `bUkKZ` bit(1) NOT NULL DEFAULT b'0',
  `bUkZK` bit(1) NOT NULL DEFAULT b'0',
  `bUkKLP` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `kod` (`sKod`)
) ENGINE=InnoDB AUTO_INCREMENT=8800 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci ROW_FORMAT=COMPACT;

-- --------------------------------------------------------

--
-- Struktura tabulky `tblPredmetyNaObory`
--

DROP TABLE IF EXISTS `tblPredmetyNaObory`;
CREATE TABLE IF NOT EXISTS `tblPredmetyNaObory` (
  `id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT,
  `fk_tblObory` int(10) UNSIGNED NOT NULL,
  `fk_tblPredmety` int(10) UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_tblObory` (`fk_tblObory`),
  KEY `fk_tblPredmety` (`fk_tblPredmety`)
) ENGINE=InnoDB AUTO_INCREMENT=12158 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci ROW_FORMAT=COMPACT;

-- --------------------------------------------------------

--
-- Struktura tabulky `tblStudenti`
--

DROP TABLE IF EXISTS `tblStudenti`;
CREATE TABLE IF NOT EXISTS `tblStudenti` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `nIdOsoby` int(10) UNSIGNED NOT NULL,
  `sName` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sSurname` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sStreet` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sCity` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `nZipCode` int(10) UNSIGNED NOT NULL,
  `sEmail` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `xHash` binary(64) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `nIdOsoby` (`nIdOsoby`),
  UNIQUE KEY `sEmail` (`sEmail`,`nIdOsoby`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

--
-- Omezení pro exportované tabulky
--

--
-- Omezení pro tabulku `tblObory`
--
ALTER TABLE `tblObory`
  ADD CONSTRAINT `fk_tblDruhyStudia` FOREIGN KEY (`fk_tblDruhyStudia`) REFERENCES `tblDruhyStudia` (`id`);

--
-- Omezení pro tabulku `tblPredmetyNaObory`
--
ALTER TABLE `tblPredmetyNaObory`
  ADD CONSTRAINT `fk_tblObory` FOREIGN KEY (`fk_tblObory`) REFERENCES `tblObory` (`id`),
  ADD CONSTRAINT `fk_tblPredmety` FOREIGN KEY (`fk_tblPredmety`) REFERENCES `tblPredmety` (`id`);
