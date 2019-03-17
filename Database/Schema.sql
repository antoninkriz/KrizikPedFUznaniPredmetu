SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";
CREATE DATABASE IF NOT EXISTS `KarolinkaDb` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_520_ci;
USE `KarolinkaDb`;

DROP TABLE IF EXISTS `tblDruhyStudia`;
CREATE TABLE IF NOT EXISTS `tblDruhyStudia` (
  `pk_tblDruhyStudia` binary(16) NOT NULL,
  `sZkratka` varchar(5) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sNazev` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  PRIMARY KEY (`pk_tblDruhyStudia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

DROP TABLE IF EXISTS `tblKatedry`;
CREATE TABLE IF NOT EXISTS `tblKatedry` (
  `pk_tblKatedry` binary(16) NOT NULL,
  `sKod` varchar(16) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  PRIMARY KEY (`pk_tblKatedry`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

DROP TABLE IF EXISTS `tblObory`;
CREATE TABLE IF NOT EXISTS `tblObory` (
  `pk_tblObory` binary(16) NOT NULL,
  `fk_tblDruhyStudia` binary(16) NOT NULL,
  `sKod` varchar(16) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sNazev` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sPoznamka` varchar(255) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `nRokZacatek` int(10) UNSIGNED DEFAULT NULL,
  `nRokUkonceni` int(10) UNSIGNED DEFAULT NULL,
  `bFormaStudia` bit(1) NOT NULL,
  PRIMARY KEY (`pk_tblObory`),
  UNIQUE KEY `pk_tblObory_UNIQUE` (`pk_tblObory`),
  KEY `fk_tblObory_druhyStudia_idx` (`fk_tblDruhyStudia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

DROP TABLE IF EXISTS `tblPredmety`;
CREATE TABLE IF NOT EXISTS `tblPredmety` (
  `pk_tblPredmety` binary(16) NOT NULL,
  `fk_tblKatedra` binary(16) NOT NULL,
  `sKod` varchar(16) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `sNazev` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `nKredity` int(11) NOT NULL,
  `bUkZ` bit(1) NOT NULL,
  `bUkKZ` bit(1) NOT NULL,
  `bUkZK` bit(1) NOT NULL,
  `bUkKLP` bit(1) NOT NULL,
  PRIMARY KEY (`pk_tblPredmety`),
  UNIQUE KEY `pk_tblPredmety_UNIQUE` (`pk_tblPredmety`),
  KEY `fk_tblPredmety_katedra_idx` (`fk_tblKatedra`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

DROP TABLE IF EXISTS `tblPredmetyNaObory`;
CREATE TABLE IF NOT EXISTS `tblPredmetyNaObory` (
  `fk_tblObory` binary(16) NOT NULL,
  `fk_tblPredmety` binary(16) NOT NULL,
  KEY `fk_tblPredmetyNaObory_1_idx` (`fk_tblObory`),
  KEY `fk_tblPredmetyNaObory_predmety_idx` (`fk_tblPredmety`),
  KEY `fk_tblObory` (`fk_tblObory`),
  KEY `fk_tblPredmety` (`fk_tblPredmety`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;


ALTER TABLE `tblObory`
  ADD CONSTRAINT `fk_tblObory_druhyStudia` FOREIGN KEY (`fk_tblDruhyStudia`) REFERENCES `tblDruhyStudia` (`pk_tblDruhyStudia`) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `tblPredmety`
  ADD CONSTRAINT `fk_tblPredmety_katedra` FOREIGN KEY (`fk_tblKatedra`) REFERENCES `tblKatedry` (`pk_tblKatedry`) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `tblPredmetyNaObory`
  ADD CONSTRAINT `fk_tblPredmetyNaObory_obory` FOREIGN KEY (`fk_tblObory`) REFERENCES `tblObory` (`pk_tblObory`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_tblPredmetyNaObory_predmety` FOREIGN KEY (`fk_tblPredmety`) REFERENCES `tblPredmety` (`pk_tblPredmety`) ON DELETE NO ACTION ON UPDATE NO ACTION;
