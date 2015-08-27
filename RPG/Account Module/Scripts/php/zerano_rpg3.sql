-- phpMyAdmin SQL Dump
-- version 4.3.8
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Erstellungszeit: 27. Mrz 2015 um 15:00
-- Server-Version: 5.5.40-36.1
-- PHP-Version: 5.4.23

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Datenbank: `zerano_rpg3`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `account`
--

CREATE TABLE IF NOT EXISTS `account` (
  `id` int(11) NOT NULL,
  `user` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `password` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `active` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Daten für Tabelle `account`
--

INSERT INTO `account` (`id`, `user`, `password`, `email`, `active`) VALUES
(1, 'Zerano', '098f6bcd4621d373cade4e832627b4f6', 'konstantin.janson@freenet.de', NULL),
(2, 'Zaikace', '5693723b8f09ca845417142a1381e0c0', 'wupe@list.ru', NULL);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `attributes`
--

CREATE TABLE IF NOT EXISTS `attributes` (
  `id` int(11) NOT NULL,
  `player` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `item` varchar(245) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=2257 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Daten für Tabelle `attributes`
--

INSERT INTO `attributes` (`id`, `player`, `item`) VALUES
(1905, 'Tharon', '2#Health;50;120;50/Mana;27;90;27/Level;3;99;3/Exp;100;101;0/Gold;9999;9999;500/'),
(2142, 'Zerano', '3#Health;51;120;58/Mana;27;90;27/Level;5;99;5/Exp;300;404;0/Gold;9999;9999;377/'),
(2195, 'Ð²Ð°Ð°Ð²Ñ‹', '2#Health;48;120;6/Mana;27;90;27/Level;1;99;1/Exp;100;101;0/Gold;9999;9999;500/'),
(2256, 'Tsiisus', '2#Health;48;120;57/Mana;27;90;30/Level;1;99;1/Exp;50;101;0/Gold;9999;9999;500/'),
(2231, 'dunno', '2#Health;48;120;48/Mana;27;90;27/Level;1;99;1/Exp;0;101;0/Gold;9999;9999;500/'),
(2229, 'ggg', '0#Health;50;120;27/Mana;27;90;27/Level;1;99;1/Exp;100;101;0/Gold;9999;9999;500/'),
(2251, 'Ñ‹Ñ„Ð²', '2#Health;48;120;12/Mana;27;90;27/Level;1;99;1/Exp;100;101;0/Gold;9999;9999;500/');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `container`
--

CREATE TABLE IF NOT EXISTS `container` (
  `id` int(11) NOT NULL,
  `player` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `item` varchar(245) COLLATE utf8_unicode_ci NOT NULL,
  `containerid` int(11) NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=23858 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Daten für Tabelle `container`
--

INSERT INTO `container` (`id`, `player`, `item`, `containerid`) VALUES
(22447, 'Zerano', '0;Stone;4/1;Axe;1;Health.1#Mana.-1#/11;Health Potion;3/', 8),
(19951, 'Tharon', '5;Axe;1;Health.-4#Mana.7#/', 3),
(22444, 'Zerano', '1;Healing/4;Arcane Ball/', 1),
(22446, 'Zerano', '0;Nail;11/1;Nail;3/2;Wood;11/3;Spade;1;/4;Nail;20/5;Wood;20/12;Stone;8/', 0),
(23096, 'Ð²Ð°Ð°Ð²Ñ‹', '0;Stone;2/1;Wood;3/2;Health Potion;2/4;Axe;1;Health.-5#Mana.-10#/', 0),
(19949, 'Tharon', '0;Arcane Ball/1;Healing/', 1),
(23098, 'Ð²Ð°Ð°Ð²Ñ‹', '5;Axe;1;Health.-8#Mana.4#/', 3),
(22443, 'Zerano', '0;Healing/1;Arcane Ball/', 2),
(19950, 'Tharon', '0;Health Potion;2/1;Axe;1;Health.-8#Mana.-9#/', 0),
(19953, 'Tharon', '', 8),
(19952, 'Tharon', '0;Healing/1;Arcane Ball/', 2),
(22445, 'Zerano', '5;Axe;1;Health.7#Mana.1#/', 3),
(23097, 'Ð²Ð°Ð°Ð²Ñ‹', '0;Healing/1;Arcane Ball/', 2),
(23538, 'dunno', '', 4),
(23100, 'Ð²Ð°Ð°Ð²Ñ‹', '0;Healing/1;Healing/2;Arcane Ball/3;Arcane Ball/', 1),
(23857, 'Tsiisus', '', 3),
(23099, 'Ð²Ð°Ð°Ð²Ñ‹', '', 8),
(23855, 'Tsiisus', '', 4),
(23853, 'Tsiisus', '0;Stone;4/1;Health Potion;4/2;Wood;5/3;Axe;1;Health.-7#Mana.-3#/5;Axe;1;Health.9#Mana.-6#/', 0),
(23854, 'Tsiisus', '', 8),
(23101, 'Ð²Ð°Ð°Ð²Ñ‹', '0;Axe;1;Health.0#Mana.0#/1;Hammer;1;/2;Spade;1;/', 4),
(23506, 'ggg', '0;Health Potion;2/1;Axe;1;Health.3#Mana.-7#/', 0),
(23505, 'ggg', '0;Health Potion;2/1;Stone;1/2;Stone;2/', 8),
(23507, 'ggg', '0;Arcane Ball/8;Healing/', 1),
(23782, 'Ñ‹Ñ„Ð²', '3;Arcane Ball/4;Arcane Ball/5;Arcane Ball/6;Arcane Ball/7;Arcane Ball/8;Healing/9;Healing/', 1),
(23780, 'Ñ‹Ñ„Ð²', '0;Healing/1;Arcane Ball/', 2),
(23783, 'Ñ‹Ñ„Ð²', '5;Spade;1;/', 3),
(23781, 'Ñ‹Ñ„Ð²', '0;Health Potion;3/1;Axe;1;Health.-6#Mana.7#/2;Stone;4/3;Hammer;1;/', 0),
(23785, 'Ñ‹Ñ„Ð²', '', 8),
(23508, 'ggg', '', 4),
(23509, 'ggg', '0;Healing/1;Arcane Ball/', 2),
(23504, 'ggg', '', 3),
(23537, 'dunno', '', 8),
(23539, 'dunno', '', 3),
(23534, 'dunno', '0;Healing/1;Arcane Ball/', 2),
(23535, 'dunno', '', 0),
(23536, 'dunno', '3;Arcane Ball/4;Arcane Ball/5;Healing/6;Healing/7;Healing/8;Healing/', 1),
(23784, 'Ñ‹Ñ„Ð²', '0;Axe;1;Health.0#Mana.0#/1;Hammer;1;/2;Spade;1;/', 4),
(23856, 'Tsiisus', '0;Healing/1;Arcane Ball/2;Whirl Wind/3;Thorn Storm/4;Mortal Strike/', 2),
(23852, 'Tsiisus', '3;Healing/4;Arcane Ball/', 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `player`
--

CREATE TABLE IF NOT EXISTS `player` (
  `id` int(11) NOT NULL,
  `name` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `level` int(11) NOT NULL,
  `custom` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `account` varchar(45) COLLATE utf8_unicode_ci NOT NULL
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Daten für Tabelle `player`
--

INSERT INTO `player` (`id`, `name`, `level`, `custom`, `account`) VALUES
(1, 'Zerano', 1, 'Warlock', 'Zerano'),
(2, 'Zaikace', 1, 'Warlock', 'Zaikace'),
(3, 'Tharon', 1, 'Warrior', 'Zerano');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `attributes`
--
ALTER TABLE `attributes`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `container`
--
ALTER TABLE `container`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `player`
--
ALTER TABLE `player`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `account`
--
ALTER TABLE `account`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT für Tabelle `attributes`
--
ALTER TABLE `attributes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2257;
--
-- AUTO_INCREMENT für Tabelle `container`
--
ALTER TABLE `container`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=23858;
--
-- AUTO_INCREMENT für Tabelle `player`
--
ALTER TABLE `player`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=4;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
