<?php
$connection = mysqli_connect("localhost", "root", "", "dungeonmaster");

if (mysqli_connect_errno()) {
    exit("1: Connection failed");
}

$username = $_POST["username"];
$password = $_POST["pass"];
$email = $_POST["email"];

$hash = password_hash($password, PASSWORD_DEFAULT);

$nameCheckQuery = "SELECT Username FROM players WHERE Username = ?";
$stmt = mysqli_prepare($connection, $nameCheckQuery);
mysqli_stmt_bind_param($stmt, "s", $username);
mysqli_stmt_execute($stmt);
mysqli_stmt_store_result($stmt);

if (mysqli_stmt_num_rows($stmt) > 0) {
    exit("3: Name already exists");
}
mysqli_stmt_close($stmt);

$emailCheckQuery = "SELECT Email FROM players WHERE Email = ?";
$stmt = mysqli_prepare($connection, $emailCheckQuery);
mysqli_stmt_bind_param($stmt, "s", $email);
mysqli_stmt_execute($stmt);
mysqli_stmt_store_result($stmt);

if (mysqli_stmt_num_rows($stmt) > 0) {
    exit("5: Email already exists");
}
mysqli_stmt_close($stmt);

$insertUserQuery = "INSERT INTO players (Username, PasswordHash, Email) VALUES (?, ?, ?)";
$stmt = mysqli_prepare($connection, $insertUserQuery);
mysqli_stmt_bind_param($stmt, "sss", $username, $hash, $email);

if (!mysqli_stmt_execute($stmt)) {
    exit("6: Insert player query failed");
}

$playerID = mysqli_insert_id($connection);
mysqli_stmt_close($stmt);

$insertStatsQuery = "INSERT INTO playerstats (PlayerID, Kills, Deaths, Level, Playtime, RegDate) VALUES (?, 0, 0, 1, 0, current_timestamp())";
$stmt = mysqli_prepare($connection, $insertStatsQuery);
mysqli_stmt_bind_param($stmt, "i", $playerID);

if (!mysqli_stmt_execute($stmt)) {
    exit("8: Insert playerstats query failed");
}
mysqli_stmt_close($stmt);

echo "0";
mysqli_close($connection);
?>
