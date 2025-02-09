<?php
    $connection = mysqli_connect("localhost","root","","dungeonmaster");
    if (mysqli_connect_errno()) {
        echo "1: Connection failed";
        exit();
    }

    $query = "SELECT players.Username, playerstats.Kills, playerstats.Deaths, playerstats.Level, playerstats.RegDate, playerstats.Playtime FROM players INNER JOIN playerstats ON players.PlayerID = playerstats.PlayerID";
    $result = mysqli_query($connection, $query);
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Dungeon Master</title>
    <style>
        table {
            width: 80%;
            border-collapse: collapse;
            margin: 20px auto;
            font-family: Arial, sans-serif;
        }
        th, td {
            border: 1px solid black;
            padding: 10px;
            text-align: center;
        }
        th {
            background-color: #4CAF50;
            color: white;
        }
        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <h2 style="text-align:center;">Scoreboard</h2>

    <table>
        <tr>
            <th>Username</th>
            <th>Kills</th>
            <th>Deaths</th>
            <th>Level</th>
            <th>Registration Date</th>
            <th>Playtime (min)</th>
        </tr>

    <?php
    if (mysqli_num_rows($result) > 0) {
        while ($row = mysqli_fetch_assoc($result)) {
            echo "<tr>
                    <td>{$row['Username']}</td>
                    <td>{$row['Kills']}</td>
                    <td>{$row['Deaths']}</td>
                    <td>{$row['Level']}</td>
                    <td>{$row['RegDate']}</td>
                    <td>{$row['Playtime']}</td>
                  </tr>";
        }
    } else {
        echo "<tr><td colspan='5'>Nincsenek statisztikák.</td></tr>";
    }
    ?>

</table>
</body>
</html>