<?php
    $connection = mysqli_connect("localhost","root","","dungeonmaster");
    if (mysqli_connect_errno()) {
        echo "1: Connection failed";
        exit();
    } 

    $username = $_POST["username"];
    $password = $_POST["pass"];

    $sql = "SELECT PasswordHash FROM players WHERE Username = ?;";
    $stmt = mysqli_prepare($connection, $sql);
    mysqli_stmt_bind_param($stmt, "s", $username);
    mysqli_stmt_execute($stmt);
    $result = mysqli_stmt_get_result($stmt);

    if ($row = mysqli_fetch_assoc($result)) {
        if (password_verify($password, $row['PasswordHash'])) {
            echo "0";
        } else {
            echo "3: Incorrect password";
        }
    } else {
        echo "2: User not found";
    }

    mysqli_close($connection);