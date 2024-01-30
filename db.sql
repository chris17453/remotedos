-- MySQL dump 10.15  Distrib 10.0.23-MariaDB, for Linux (x86_64)
--
-- Host: localhost    Database: dm-remote-dos
-- ------------------------------------------------------
-- Server version	10.0.23-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `_applications`
--

DROP TABLE IF EXISTS `_applications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_applications` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `uid` varchar(255) COLLATE utf8_bin NOT NULL,
  `server` varchar(255) COLLATE utf8_bin NOT NULL,
  `pid` int(11) NOT NULL,
  `created` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `idle` datetime NOT NULL,
  `type` varchar(255) COLLATE utf8_bin DEFAULT '',
  `printJob` varchar(30) COLLATE utf8_bin DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=262 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_commands`
--

DROP TABLE IF EXISTS `_commands`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_commands` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `uid` varchar(255) COLLATE utf8_bin NOT NULL,
  `pid` int(11) NOT NULL,
  `server` varchar(20) COLLATE utf8_bin NOT NULL,
  `type` enum('application','keystroke') COLLATE utf8_bin DEFAULT 'keystroke',
  `code` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `shift` varchar(1) COLLATE utf8_bin NOT NULL,
  `alt` varchar(1) COLLATE utf8_bin NOT NULL,
  `ctrl` varchar(1) COLLATE utf8_bin NOT NULL,
  `keyState1` int(10) unsigned DEFAULT NULL,
  `keyState2` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=88519 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_elua`
--

DROP TABLE IF EXISTS `_elua`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_elua` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `elua` text COLLATE utf8_bin NOT NULL,
  `licence` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `perServerLimit` int(11) DEFAULT NULL,
  `clusterInstances` int(11) DEFAULT NULL,
  `clientInstances` int(11) DEFAULT NULL,
  `licenceType` enum('Network','Demo','Server','Single User') COLLATE utf8_bin DEFAULT 'Demo',
  `licenceBegin` date DEFAULT NULL,
  `licenceEnd` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_firewall`
--

DROP TABLE IF EXISTS `_firewall`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_firewall` (
  `block` varchar(1) NOT NULL DEFAULT 'N',
  PRIMARY KEY (`block`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_messages`
--

DROP TABLE IF EXISTS `_messages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_messages` (
  `id` int(11) NOT NULL,
  `uid` varchar(255) DEFAULT NULL,
  `title` varchar(255) DEFAULT NULL,
  `message` varchar(255) DEFAULT NULL,
  `created` datetime NOT NULL,
  `read` varchar(1) DEFAULT 'N',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_mouse`
--

DROP TABLE IF EXISTS `_mouse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_mouse` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `uid` varchar(255) COLLATE utf8_bin NOT NULL,
  `pid` int(11) NOT NULL,
  `server` varchar(255) COLLATE utf8_bin NOT NULL,
  `left` int(11) NOT NULL,
  `middle` int(11) NOT NULL,
  `right` int(11) NOT NULL,
  `x` int(11) NOT NULL,
  `y` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_preferences`
--

DROP TABLE IF EXISTS `_preferences`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_preferences` (
  `userID` int(11) NOT NULL,
  `autoPrint` varchar(1) COLLATE utf8_bin NOT NULL DEFAULT 'Y',
  `size` int(11) NOT NULL,
  `width` int(11) NOT NULL,
  `height` int(11) NOT NULL,
  `conserveBW` varchar(1) COLLATE utf8_bin NOT NULL DEFAULT 'Y',
  PRIMARY KEY (`userID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_print`
--

DROP TABLE IF EXISTS `_print`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_print` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `pid` int(11) NOT NULL,
  `server` varchar(255) COLLATE utf8_bin NOT NULL,
  `uid` varchar(255) COLLATE utf8_bin NOT NULL,
  `document` longtext COLLATE utf8_bin,
  `created` datetime DEFAULT NULL,
  `printed` int(11) DEFAULT NULL,
  `filename` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `path` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pid` (`pid`,`server`),
  KEY `ind2` (`created`,`uid`)
) ENGINE=InnoDB AUTO_INCREMENT=137120 DEFAULT CHARSET=utf8 COLLATE=utf8_bin ROW_FORMAT=COMPRESSED;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_servers`
--

DROP TABLE IF EXISTS `_servers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_servers` (
  `server` varchar(255) COLLATE utf8_bin NOT NULL,
  `max` int(11) NOT NULL,
  PRIMARY KEY (`server`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_sessions`
--

DROP TABLE IF EXISTS `_sessions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_sessions` (
  `pid` int(11) NOT NULL,
  `server` varchar(20) COLLATE utf8_bin NOT NULL,
  `screen` varchar(8000) COLLATE utf8_bin NOT NULL,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `cursorx` smallint(6) NOT NULL DEFAULT '0',
  `cursory` smallint(6) NOT NULL,
  `pulled` varchar(1) COLLATE utf8_bin NOT NULL DEFAULT 'N',
  PRIMARY KEY (`pid`,`server`),
  UNIQUE KEY `pid` (`pid`,`server`) USING HASH
) ENGINE=MEMORY DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_sessions_b`
--

DROP TABLE IF EXISTS `_sessions_b`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_sessions_b` (
  `pid` int(11) NOT NULL,
  `server` varchar(20) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `screen` varbinary(4000) NOT NULL,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `cursorx` smallint(6) NOT NULL DEFAULT '0',
  `cursory` smallint(6) NOT NULL,
  `pulled` varchar(1) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL DEFAULT 'N',
  PRIMARY KEY (`pid`,`server`),
  UNIQUE KEY `pid` (`pid`,`server`) USING HASH
) ENGINE=MEMORY DEFAULT CHARSET=ascii;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `_users`
--

DROP TABLE IF EXISTS `_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `_users` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `user` varchar(255) COLLATE utf8_bin NOT NULL,
  `domain` varchar(255) COLLATE utf8_bin NOT NULL,
  `ip` varchar(20) COLLATE utf8_bin NOT NULL,
  `active` varchar(1) COLLATE utf8_bin NOT NULL,
  `created` datetime NOT NULL,
  `lastlogon` datetime NOT NULL,
  `appServer` varchar(50) COLLATE utf8_bin NOT NULL DEFAULT 'test',
  `appPass` varchar(20) COLLATE utf8_bin DEFAULT NULL,
  `appLogin` varchar(20) COLLATE utf8_bin DEFAULT NULL,
  `whiteListed` varchar(1) COLLATE utf8_bin DEFAULT 'N',
  PRIMARY KEY (`user`,`domain`),
  KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=426619 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-30  6:45:42
