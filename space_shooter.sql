-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Jan 26, 2018 at 12:50 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `space_shooter`
--
CREATE DATABASE IF NOT EXISTS `space_shooter` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `space_shooter`;

-- --------------------------------------------------------

--
-- Table structure for table `friends`
--

CREATE TABLE `friends` (
  `player_1_id` int(11) DEFAULT NULL,
  `player_2_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `friends`
--

INSERT INTO `friends` (`player_1_id`, `player_2_id`) VALUES
(13, 12);

-- --------------------------------------------------------

--
-- Table structure for table `game_stats`
--

CREATE TABLE `game_stats` (
  `player_id` int(11) DEFAULT NULL,
  `score` bigint(20) DEFAULT NULL,
  `enemies_destroyed` int(11) DEFAULT NULL,
  `game_time` bigint(20) DEFAULT NULL,
  `date_played` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `game_stats`
--

INSERT INTO `game_stats` (`player_id`, `score`, `enemies_destroyed`, `game_time`, `date_played`) VALUES
(12, 900, 9, 11441, '2017-11-09 15:54:27'),
(12, 1400, 14, 10778, '2017-11-09 15:55:02'),
(12, 4300, 43, 22537, '2017-11-09 15:55:28'),
(12, 900, 9, 10181, '2017-11-09 15:55:45'),
(12, 1900, 19, 15899, '2017-11-09 15:56:42'),
(12, 700, 7, 12199, '2017-11-09 15:57:06'),
(13, 800, 8, 12421, '2017-11-09 16:08:16'),
(14, 2700, 27, 18630, '2017-11-09 16:13:32'),
(14, 12200, 122, 44943, '2017-11-09 16:14:26'),
(14, 1000, 10, 9340, '2017-11-09 16:15:48'),
(14, 6500, 65, 34808, '2017-11-09 16:16:27'),
(14, 2000, 20, 15091, '2017-11-09 16:18:57'),
(14, 8000, 80, 34760, '2017-11-09 16:22:07'),
(15, 9000, 90, 38928, '2017-11-09 16:25:29'),
(16, 5400, 54, 35169, '2017-11-09 16:27:00'),
(17, 3100, 31, 20350, '2017-11-09 16:28:17'),
(17, 1600, 16, 20920, '2017-11-09 16:29:37'),
(18, 13400, 134, 48111, '2017-11-09 16:45:08'),
(19, 1600, 16, 22219, '2018-01-25 15:20:23');

-- --------------------------------------------------------

--
-- Table structure for table `players`
--

CREATE TABLE `players` (
  `id` int(11) NOT NULL,
  `login_name` varchar(255) DEFAULT NULL,
  `password_hash` varchar(255) DEFAULT NULL,
  `salt` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `players`
--

INSERT INTO `players` (`id`, `login_name`, `password_hash`, `salt`) VALUES
(12, 'user', 'ba1375007accc2a90b3189f480935519080e448ac0', '7a0876cf7732fa924965131fabc59dee547df1140c'),
(13, 'epicodus', '961c89de7469f0290d595be9b0e6e5837d742b5629', 'ed90c6e78ced63530fb2f3c994f7effd288bc5b9a4'),
(14, 'madmax', '0957f9afa45bb3251089a8276143a21e6118825770', 'd477f85450bfcf35b564235170c04af98a64bff745'),
(15, 'jim', '00292c952b3eefc95c4090d2e771bf537e26d75ddd', '333efb11c3d555b12551be1b93ca0bd8e6584e2cc4'),
(16, 'javi', 'd519047f46e412900c9af2e910fe8063972787ab32', '1ab66c96418d71b2cd4a8458d0445cb839047206fb'),
(17, 'phuz', '0db7f1717167ebb2312d43f3431d97fc60e79562aa', '96edbb6acb1f46d2e18202fd4f384dc6066596441b'),
(18, 'Rane', '9246e31f0b7b188f13a8e0d9b1321675e0ce4804e5', 'ca2de2b62987dc8fcf96a078bc4bfe3030c1b37be8'),
(19, 'AdamTitus', '06fa580540af76586f39b013220fec6f7343ebe16c', '8ce570a6477adcfc3d50b63291aa3342f1f80a4eae');

-- --------------------------------------------------------

--
-- Table structure for table `profiles`
--

CREATE TABLE `profiles` (
  `player_id` int(11) DEFAULT NULL,
  `screen_name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `sessions`
--

CREATE TABLE `sessions` (
  `session_id` varchar(255) DEFAULT NULL,
  `player_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `sessions`
--

INSERT INTO `sessions` (`session_id`, `player_id`) VALUES
('ea58ace06d48f8678d29dc0bf922031b4b90e21f27', 18),
('921fc9be02adbc6ec199e1096fcff94240443142d9', 19);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `friends`
--
ALTER TABLE `friends`
  ADD KEY `player_1_id` (`player_1_id`),
  ADD KEY `player_2_id` (`player_2_id`);

--
-- Indexes for table `game_stats`
--
ALTER TABLE `game_stats`
  ADD KEY `player_id` (`player_id`);

--
-- Indexes for table `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `profiles`
--
ALTER TABLE `profiles`
  ADD KEY `player_id` (`player_id`);

--
-- Indexes for table `sessions`
--
ALTER TABLE `sessions`
  ADD KEY `player_id` (`player_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `players`
--
ALTER TABLE `players`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `friends`
--
ALTER TABLE `friends`
  ADD CONSTRAINT `friends_ibfk_1` FOREIGN KEY (`player_1_id`) REFERENCES `players` (`id`),
  ADD CONSTRAINT `friends_ibfk_2` FOREIGN KEY (`player_2_id`) REFERENCES `players` (`id`);

--
-- Constraints for table `game_stats`
--
ALTER TABLE `game_stats`
  ADD CONSTRAINT `game_stats_ibfk_1` FOREIGN KEY (`player_id`) REFERENCES `players` (`id`);

--
-- Constraints for table `profiles`
--
ALTER TABLE `profiles`
  ADD CONSTRAINT `profiles_ibfk_1` FOREIGN KEY (`player_id`) REFERENCES `players` (`id`);

--
-- Constraints for table `sessions`
--
ALTER TABLE `sessions`
  ADD CONSTRAINT `sessions_ibfk_1` FOREIGN KEY (`player_id`) REFERENCES `players` (`id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
