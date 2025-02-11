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
    }
</style>
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
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>