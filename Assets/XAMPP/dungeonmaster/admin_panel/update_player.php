<?php
require '../db.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $playerID = $_POST['PlayerID'];
    $username = $_POST['username'];
    $email = $_POST['email'];

    $sql = "UPDATE players SET Username = :username, Email = :email WHERE PlayerID = :playerID";
    $stmt = $conn->prepare($sql);
    $stmt->execute([
        ':username' => $username,
        ':email' => $email,
        ':playerID' => $playerID
    ]);

    header('Location: users.php');
    exit;
}
?>