-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Már 14. 08:58
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `dungeonmaster`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `matchlogs`
--

CREATE TABLE `matchlogs` (
  `MatchID` int(11) NOT NULL,
  `PlayerID` int(11) NOT NULL,
  `Kills` int(11) DEFAULT 0,
  `MatchDuration` int(11) NOT NULL,
  `Win` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `matchlogs`
--

INSERT INTO `matchlogs` (`MatchID`, `PlayerID`, `Kills`, `MatchDuration`, `Win`) VALUES
(1, 2, 999, 33, 1),
(3, 4, 15, 18, 1),
(4, 5, 6, 22, 0),
(5, 6, 10, 25, 1),
(6, 7, 7, 19, 0),
(7, 8, 14, 17, 1),
(8, 9, 9, 21, 0),
(9, 10, 11, 16, 1),
(10, 11, 5, 23, 0),
(11, 12, 13, 14, 1),
(12, 13, 4, 24, 0),
(13, 14, 16, 13, 1),
(14, 15, 3, 26, 0),
(15, 16, 17, 12, 1),
(16, 17, 2, 27, 0),
(17, 18, 18, 11, 1),
(18, 19, 1, 28, 0),
(19, 20, 19, 10, 1),
(20, 21, 0, 29, 0),
(21, 22, 20, 9, 1),
(22, 23, 1, 30, 0),
(23, 24, 21, 8, 1),
(24, 25, 2, 31, 0),
(25, 26, 22, 7, 1),
(26, 27, 3, 32, 0),
(27, 28, 23, 6, 1),
(28, 29, 4, 33, 0),
(29, 30, 24, 5, 1),
(30, 31, 5, 34, 0),
(31, 32, 25, 4, 1),
(32, 33, 6, 35, 0),
(33, 34, 26, 3, 1),
(34, 35, 7, 36, 0),
(35, 36, 27, 2, 1),
(36, 37, 8, 37, 0),
(37, 38, 28, 1, 1),
(38, 39, 9, 38, 0),
(39, 40, 29, 2, 1),
(40, 41, 10, 39, 0),
(41, 42, 30, 3, 1),
(43, 4, 31, 4, 1),
(44, 5, 12, 41, 0),
(45, 6, 32, 5, 1),
(46, 7, 13, 42, 0),
(47, 8, 33, 6, 1),
(48, 9, 14, 43, 0),
(49, 10, 34, 7, 1),
(50, 11, 15, 44, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `players`
--

CREATE TABLE `players` (
  `PlayerID` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Level` int(3) NOT NULL DEFAULT 0,
  `RegDate` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `players`
--

INSERT INTO `players` (`PlayerID`, `Username`, `PasswordHash`, `Email`, `Level`, `RegDate`) VALUES
(2, 'sango', '$2y$10$xauXn7cweWIFLW.Uuu9zWOMBSpAWWt4rTNdjFzCO0HNAKN.Ec8zxu', 'sango@sango.sango', 100, '2025-03-10 00:00:00'),
(4, 'bob456', '$2y$10$abcd5678efgh90ijklmnopqrstuvwxyz1234', 'bob456@example.com', 10, '2023-02-20 14:22:33'),
(5, 'charlie789', '$2y$10$efgh90ijklmnopqrstuvwxyz1234abcd5678', 'charlie789@example.com', 3, '2023-03-25 09:45:12'),
(6, 'dave101', '$2y$10$ijklmnopqrstuvwxyz1234abcd5678efgh90', 'dave101@example.com', 7, '2023-04-30 18:11:44'),
(7, 'eve202', '$2y$10$klmnopqrstuvwxyz1234abcd5678efgh90ij', 'eve202@example.com', 12, '2023-05-05 22:33:55'),
(8, 'frank303', '$2y$10$mnopqrstuvwxyz1234abcd5678efgh90ijkl', 'frank303@example.com', 8, '2023-06-10 07:59:01'),
(9, 'grace404', '$2y$10$nopqrstuvwxyz1234abcd5678efgh90ijklmn', 'grace404@example.com', 15, '2023-07-15 13:14:16'),
(10, 'hank505', '$2y$10$opqrstuvwxyz1234abcd5678efgh90ijklmno', 'hank505@example.com', 6, '2023-08-20 19:20:21'),
(11, 'ivy606', '$2y$10$pqrstuvwxyz1234abcd5678efgh90ijklmnop', 'ivy606@example.com', 9, '2023-09-25 23:45:34'),
(12, 'jack707', '$2y$10$qrstuvwxyz1234abcd5678efgh90ijklmnopq', 'jack707@example.com', 4, '2023-10-30 04:56:47'),
(13, 'kate808', '$2y$10$rstuvwxyz1234abcd5678efgh90ijklmnopqr', 'kate808@example.com', 11, '2023-11-05 10:07:58'),
(14, 'leo909', '$2y$10$stuvwxyz1234abcd5678efgh90ijklmnopqrs', 'leo909@example.com', 2, '2023-12-10 15:18:09'),
(15, 'mia1010', '$2y$10$tuvwxyz1234abcd5678efgh90ijklmnopqrstu', 'mia1010@example.com', 14, '2024-01-15 20:29:10'),
(16, 'noah1111', '$2y$10$uvwxyz1234abcd5678efgh90ijklmnopqrstuv', 'noah1111@example.com', 7, '2024-02-20 01:40:11'),
(17, 'olivia1212', '$2y$10$vwxyz1234abcd5678efgh90ijklmnopqrstuvw', 'olivia1212@example.com', 10, '2024-03-25 06:51:12'),
(18, 'peter1313', '$2y$10$wxyz1234abcd5678efgh90ijklmnopqrstuvwx', 'peter1313@example.com', 5, '2024-04-30 12:02:13'),
(19, 'quinn1414', '$2y$10$xyz1234abcd5678efgh90ijklmnopqrstuvwxy', 'quinn1414@example.com', 8, '2024-05-05 17:13:14'),
(20, 'rachel1515', '$2y$10$yz1234abcd5678efgh90ijklmnopqrstuvwxyz', 'rachel1515@example.com', 13, '2024-06-10 22:24:15'),
(21, 'sam1616', '$2y$10$z1234abcd5678efgh90ijklmnopqrstuvwxyz1', 'sam1616@example.com', 6, '2024-07-15 03:35:16'),
(22, 'tina1717', '$2y$10$1234abcd5678efgh90ijklmnopqrstuvwxyz12', 'tina1717@example.com', 9, '2024-08-20 08:46:17'),
(23, 'uma1818', '$2y$10$234abcd5678efgh90ijklmnopqrstuvwxyz123', 'uma1818@example.com', 4, '2024-09-25 13:57:18'),
(24, 'vince1919', '$2y$10$34abcd5678efgh90ijklmnopqrstuvwxyz1234', 'vince1919@example.com', 11, '2024-10-30 19:08:19'),
(25, 'willa2020', '$2y$10$4abcd5678efgh90ijklmnopqrstuvwxyz12345', 'willa2020@example.com', 16, '2024-11-05 00:19:20'),
(26, 'xander2121', '$2y$10$abcd5678efgh90ijklmnopqrstuvwxyz123456', 'xander2121@example.com', 3, '2024-12-10 05:30:21'),
(27, 'yara2222', '$2y$10$bcd5678efgh90ijklmnopqrstuvwxyz1234567', 'yara2222@example.com', 7, '2025-01-15 10:41:22'),
(28, 'zack2323', '$2y$10$cd5678efgh90ijklmnopqrstuvwxyz12345678', 'zack2323@example.com', 12, '2025-02-20 15:52:23'),
(29, 'amy2424', '$2y$10$d5678efgh90ijklmnopqrstuvwxyz123456789', 'amy2424@example.com', 5, '2025-03-25 21:03:24'),
(30, 'ben2525', '$2y$10$5678efgh90ijklmnopqrstuvwxyz1234567890', 'ben2525@example.com', 8, '2025-04-30 02:14:25'),
(31, 'cara2626', '$2y$10$678efgh90ijklmnopqrstuvwxyz12345678901', 'cara2626@example.com', 10, '2025-05-05 07:25:26'),
(32, 'drew2727', '$2y$10$78efgh90ijklmnopqrstuvwxyz123456789012', 'drew2727@example.com', 6, '2025-06-10 12:36:27'),
(33, 'ella2828', '$2y$10$8efgh90ijklmnopqrstuvwxyz1234567890123', 'ella2828@example.com', 9, '2025-07-15 17:47:28'),
(34, 'finn2929', '$2y$10$efgh90ijklmnopqrstuvwxyz12345678901234', 'finn2929@example.com', 14, '2025-08-20 22:58:29'),
(35, 'gina3030', '$2y$10$fgh90ijklmnopqrstuvwxyz123456789012345', 'gina3030@example.com', 7, '2025-09-25 04:09:30'),
(36, 'hugo3131', '$2y$10$gh90ijklmnopqrstuvwxyz1234567890123456', 'hugo3131@example.com', 11, '2025-10-30 09:20:31'),
(37, 'isla3232', '$2y$10$h90ijklmnopqrstuvwxyz12345678901234567', 'isla3232@example.com', 4, '2025-11-05 14:31:32'),
(38, 'jake3333', '$2y$10$90ijklmnopqrstuvwxyz123456789012345678', 'jake3333@example.com', 8, '2025-12-10 19:42:33'),
(39, 'kira3434', '$2y$10$0ijklmnopqrstuvwxyz1234567890123456789', 'kira3434@example.com', 13, '2026-01-15 00:53:34'),
(40, 'luke3535', '$2y$10$ijklmnopqrstuvwxyz12345678901234567890', 'luke3535@example.com', 5, '2026-02-20 06:04:35'),
(41, 'maya3636', '$2y$10$jklmnopqrstuvwxyz123456789012345678901', 'maya3636@example.com', 9, '2026-03-25 11:15:36'),
(42, 'nora3737', '$2y$10$klmnopqrstuvwxyz1234567890123456789012', 'nora3737@example.com', 12, '2026-04-30 16:26:37'),
(43, 'admin', '$2y$10$z6SzX6dPpboTXqXr4/J/auZB/E8psUsCVNj.eiS.Vf1n47IYKuFjS', 'admin@admin.admin', 0, '2025-03-13 09:52:39');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `matchlogs`
--
ALTER TABLE `matchlogs`
  ADD PRIMARY KEY (`MatchID`),
  ADD KEY `PlayerID` (`PlayerID`);

--
-- A tábla indexei `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`PlayerID`),
  ADD UNIQUE KEY `Username` (`Username`),
  ADD UNIQUE KEY `Email` (`Email`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `matchlogs`
--
ALTER TABLE `matchlogs`
  MODIFY `MatchID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=51;

--
-- AUTO_INCREMENT a táblához `players`
--
ALTER TABLE `players`
  MODIFY `PlayerID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `matchlogs`
--
ALTER TABLE `matchlogs`
  ADD CONSTRAINT `matchlogs_ibfk_1` FOREIGN KEY (`PlayerID`) REFERENCES `players` (`PlayerID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
