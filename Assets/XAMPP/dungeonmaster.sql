-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Feb 08. 14:56
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

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

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `players`
--

CREATE TABLE `players` (
  `PlayerID` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `Email` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `players`
--

INSERT INTO `players` (`PlayerID`, `Username`, `PasswordHash`, `Email`) VALUES
(1, 'admin', '$2y$10$IoM4L5EN3/gJ5MTB8wQvBe5LvJMJ1jm0PyiVAtrOd9dwzm7llalEK', 'admin@dev.com'),
(2, 'Sango', '$2y$10$NWE6YRRIZc7VNTbZ0WY1r.g6A7yl6ejVPcSqzbxUnm6e0nKnTzOKi', 'sango@dev.com');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `playerstats`
--

CREATE TABLE `playerstats` (
  `StatID` int(11) NOT NULL,
  `PlayerID` int(11) NOT NULL,
  `Kills` int(11) DEFAULT 0,
  `Deaths` int(11) DEFAULT 0,
  `Level` int(11) DEFAULT 1,
  `Playtime` int(11) DEFAULT 0,
  `RegDate` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `playerstats`
--

INSERT INTO `playerstats` (`StatID`, `PlayerID`, `Kills`, `Deaths`, `Level`, `Playtime`, `RegDate`) VALUES
(1, 1, 0, 0, 1, 0, '2025-02-08 11:37:21'),
(2, 2, 0, 0, 1, 0, '2025-02-08 14:54:23');

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
-- A tábla indexei `playerstats`
--
ALTER TABLE `playerstats`
  ADD PRIMARY KEY (`StatID`),
  ADD UNIQUE KEY `PlayerID` (`PlayerID`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `matchlogs`
--
ALTER TABLE `matchlogs`
  MODIFY `MatchID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `players`
--
ALTER TABLE `players`
  MODIFY `PlayerID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `playerstats`
--
ALTER TABLE `playerstats`
  MODIFY `StatID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `matchlogs`
--
ALTER TABLE `matchlogs`
  ADD CONSTRAINT `matchlogs_ibfk_1` FOREIGN KEY (`PlayerID`) REFERENCES `players` (`PlayerID`) ON DELETE CASCADE;

--
-- Megkötések a táblához `playerstats`
--
ALTER TABLE `playerstats`
  ADD CONSTRAINT `playerstats_ibfk_1` FOREIGN KEY (`PlayerID`) REFERENCES `players` (`PlayerID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
