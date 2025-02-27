<?php
// db.php - Adatbázis kapcsolat létrehozása

$host = 'localhost'; // Adatbázis hoszt
$dbname = 'dungeonmaster'; // Adatbázis név
$username = 'root'; // Felhasználónév
$password = ''; // Jelszó

try {
    $conn = new PDO("mysql:host=$host;dbname=$dbname", $username, $password);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch (PDOException $e) {
    die("Adatbázis kapcsolódási hiba: " . $e->getMessage());
}
?>