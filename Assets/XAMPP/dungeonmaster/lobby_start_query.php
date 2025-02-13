<?php
    $connection = mysqli_connect("localhost","root","","dungeonmaster");
    if (mysqli_connect_errno()) {
        echo "1: Connection failed";
        exit();
    } 

    $username = $_POST["username"];

    $sql = "SELECT * FROM playerstats WHERE PlayerID = (SELECT PlayerID FROM players WHERE Username = ?);";
    $stmt = mysqli_prepare($connection, $sql);
    mysqli_stmt_bind_param($stmt, "s", $username);
    mysqli_stmt_execute($stmt);
    $result = mysqli_stmt_get_result($stmt);

    if ($row = mysqli_fetch_assoc($result)) {
        echo $row["Level"];
    } 
    else {
        echo "1: Ez kb lehetetlen";
    }

    mysqli_close($connection);
?>