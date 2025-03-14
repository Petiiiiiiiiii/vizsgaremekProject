<?php
header('Content-Type: application/json');

$connection = mysqli_connect("localhost", "root", "", "dungeonmaster");

if (mysqli_connect_errno()) {
    echo json_encode(["error" => "Hiba a kapcsolódás során!"]);
    exit();
}

$query = "
    SELECT 
        p.PlayerID, 
        p.Username, 
        IFNULL(SUM(m.Kills), 0) AS Kills, 
        IFNULL(COUNT(CASE WHEN m.Win = FALSE THEN 1 END), 0) AS Deaths, 
        p.Level, 
        p.RegDate,
        IFNULL(SUM(m.MatchDuration), 0) AS Playtime
    FROM players p
    LEFT JOIN matchlogs m ON p.PlayerID = m.PlayerID
    GROUP BY p.PlayerID
    ORDER BY Kills DESC
    LIMIT 100
";

$result = mysqli_query($connection, $query);

if (mysqli_num_rows($result) > 0) {
    $players = [];
    while ($row = mysqli_fetch_assoc($result)) {
        $players[] = $row;
    }
    echo json_encode($players);
} else {
    echo json_encode(["message" => "Nincsenek statisztikák."]);
}

mysqli_close($connection);
?>
