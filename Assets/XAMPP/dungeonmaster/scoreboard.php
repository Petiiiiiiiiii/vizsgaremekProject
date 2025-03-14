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

        .TableSort:hover {
            background-color: #321;
        }

        .sorted-asc::after {
            content: ' \2191';
        }

        .sorted-desc::after {
            content: ' \2193';
        }
    </style>
    <script>
        // Lekérjük az adatokat az API-ból
        async function fetchScoreboardData() {
            try {
                let response = await fetch('api.php');
                let data = await response.json();

                if (data.error) {
                    document.querySelector('tbody').innerHTML = `<tr><td colspan="7" class="text-danger">${data.error}</td></tr>`;
                } else if (data.message) {
                    document.querySelector('tbody').innerHTML = `<tr><td colspan="7">${data.message}</td></tr>`;
                } else {
                    populateTable(data);
                }
            } catch (error) {
                document.querySelector('tbody').innerHTML = `<tr><td colspan="7" class="text-danger">Hiba történt az adatok betöltésekor!</td></tr>`;
                document.querySelector('tbody').innerHTML = error;
            }
        }

        // Feltöltjük a táblázatot
        function populateTable(data) {
            const tbody = document.querySelector('tbody');
            tbody.innerHTML = '';

            data.forEach((row, index) => {
                const tr = document.createElement('tr');
                tr.innerHTML = `
                    <td>${index + 1}</td>
                    <td>${row.Username}</td>
                    <td>${row.Kills}</td>
                    <td>${row.Deaths}</td>
                    <td>${row.Level}</td>
                    <td>${row.Playtime}</td>
                    <td>${row.RegDate}</td>
                `;
                tbody.appendChild(tr);
            });
        }

        // Sorok rendezése
        function sortTable(columnIndex) {
            const table = document.querySelector("table");
            const tbody = table.querySelector("tbody");
            const rows = Array.from(tbody.rows);
            const isAscending = table.dataset.sortColumn == columnIndex && table.dataset.sortOrder === "asc";

            rows.sort((rowA, rowB) => {
                let cellA = rowA.cells[columnIndex].innerText.trim();
                let cellB = rowB.cells[columnIndex].innerText.trim();

                let valueA = isNaN(cellA) ? cellA.toLowerCase() : parseFloat(cellA);
                let valueB = isNaN(cellB) ? cellB.toLowerCase() : parseFloat(cellB);

                return isAscending ? (valueA > valueB ? 1 : -1) : (valueA < valueB ? 1 : -1);
            });

            table.dataset.sortColumn = columnIndex;
            table.dataset.sortOrder = isAscending ? "desc" : "asc";

            tbody.innerHTML = "";
            rows.forEach((row, index) => {
                row.cells[0].innerText = index + 1;
                tbody.appendChild(row);
            });

            document.querySelectorAll("th").forEach(th => th.classList.remove("sorted-asc", "sorted-desc"));
            document.querySelectorAll("th")[columnIndex].classList.add(isAscending ? "sorted-desc" : "sorted-asc");
        }

        // Oldal betöltésekor adatokat töltünk be
        window.onload = fetchScoreboardData;
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
                        <th>#</th>
                        <th onclick="sortTable(1)" class="TableSort">Username</th>
                        <th onclick="sortTable(2)" class="TableSort">Kills</th>
                        <th onclick="sortTable(3)" class="TableSort">Deaths</th>
                        <th onclick="sortTable(4)" class="TableSort">Level</th>
                        <th onclick="sortTable(5)" class="TableSort">Playtime (min)</th>
                        <th onclick="sortTable(6)" class="TableSort">Registration Date</th>
                    </tr>
                </thead>
                <tbody>
                    <!-- A táblázat tartalmát a fetchScoreboardData() tölti be -->
                </tbody>
            </table>
        </div>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
