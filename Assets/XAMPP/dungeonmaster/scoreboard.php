<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Dungeon Master - Scoreboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
    @font-face {
        font-family: 'DungeonFont';
        src: url('fonts/dungeon.otf') format('truetype');
        font-weight: normal;
        font-style: normal;
    }

    body {
        font-family: 'DungeonFont', sans-serif;
        background-color: #222;
        color: white;
    }
    
    .navbar {
        background-color: #111;
    }

    .navbar-brand {
        color: #FFD700 !important;
        font-weight: bold;
    }

    .nav-link {
        color: white !important;
    }

    .table {
        background-color: #333;
        color: white;
    }

    .table th {
        background-color: #444;
        cursor: pointer;
    }
    
    .TableSort:hover{
        background-color: #321;
    }
    
    .sorted-asc::after { content: ' \2191'; }
    .sorted-desc::after { content: ' \2193'; }
</style>
<script>
function sortTable(columnIndex) {
    var table = document.querySelector("table");
    var tbody = table.querySelector("tbody");
    var rows = Array.from(tbody.rows);
    var isAscending = table.dataset.sortColumn == columnIndex && table.dataset.sortOrder === "asc";

    rows.sort((rowA, rowB) => {
        var cellA = rowA.cells[columnIndex].innerText.trim();
        var cellB = rowB.cells[columnIndex].innerText.trim();

        var valueA = isNaN(cellA) ? cellA.toLowerCase() : parseFloat(cellA);
        var valueB = isNaN(cellB) ? cellB.toLowerCase() : parseFloat(cellB);

        return isAscending ? (valueA > valueB ? 1 : -1) : (valueA < valueB ? 1 : -1);
    });

    table.dataset.sortColumn = columnIndex;
    table.dataset.sortOrder = isAscending ? "desc" : "asc";

    tbody.innerHTML = "";
    rows.forEach(row => tbody.appendChild(row));

    document.querySelectorAll("th").forEach(th => th.classList.remove("sorted-asc", "sorted-desc"));
    document.querySelectorAll("th")[columnIndex].classList.add(isAscending ? "sorted-desc" : "sorted-asc");
}
</script>
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark">
        <div class="container">
            <a class="navbar-brand" href="index.php">Dungeon Master</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item"><a class="nav-link" href="index.php">Home</a></li>
                    <li class="nav-item"><a class="nav-link" href="scoreboard.php">Scoreboard</a></li>
                    <li class="nav-item"><a class="nav-link" href="registerweb.php">Register</a></li>
                    <li class="nav-item"><a class="nav-link" href="loginweb.php">Login</a></li>
                </ul>
            </div>
        </div>
    </nav>
    
    <div class="container mt-4">
        <h2 class="text-center">Scoreboard</h2>
        <div class="table-responsive">
            <table class="table table-dark table-striped text-center" data-sort-column="" data-sort-order="">
            <thead>
                <tr>
                    <th onclick="sortTable(0)" class="TableSort">Username</th>
                    <th onclick="sortTable(1)" class="TableSort">Kills</th>
                    <th onclick="sortTable(2)" class="TableSort">Deaths</th>
                    <th onclick="sortTable(3)" class="TableSort">Level</th>
                    <th onclick="sortTable(4)" class="TableSort">Playtime (min)</th>
                    <th onclick="sortTable(5)" class="TableSort">Registration Date</th>
                </tr>
            </thead>
            <tbody>
                <?php
                    $connection = mysqli_connect("localhost","root","","dungeonmaster");
                    if (mysqli_connect_errno()) {
                        echo "<tr><td colspan='6' class='text-danger'>Hiba a kapcsolódás során!</td></tr>";
                        exit();
                    }
                    $query = "SELECT players.Username, playerstats.Kills, playerstats.Deaths, playerstats.Level, playerstats.RegDate, playerstats.Playtime FROM players INNER JOIN playerstats ON players.PlayerID = playerstats.PlayerID";
                    $result = mysqli_query($connection, $query);
                    if (mysqli_num_rows($result) > 0) {
                        while ($row = mysqli_fetch_assoc($result)) {
                            echo "<tr>
                                    <td>{$row['Username']}</td>
                                    <td>{$row['Kills']}</td>
                                    <td>{$row['Deaths']}</td>
                                    <td>{$row['Level']}</td>
                                    <td>{$row['Playtime']}</td>
                                    <td>{$row['RegDate']}</td>
                                  </tr>";
                        }
                    } else {
                        echo "<tr><td colspan='6'>Nincsenek statisztikák.</td></tr>";
                    }
                    mysqli_close($connection);
                ?>
            </tbody>
            </table>
        </div>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>