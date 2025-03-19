<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Dungeon Master - Home</title>
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

        .hero-section {
            background: url('images/hero-image.jpg') no-repeat center center;
            background-size: cover;
            height: 80vh;
            display: flex;
            justify-content: center;
            align-items: center;
            text-align: center;
            color: #FFD700;
        }

        .hero-section h1 {
            font-size: 4rem;
            text-shadow: 2px 2px 5px rgba(0, 0, 0, 0.7);
        }

        .hero-section p {
            font-size: 1.5rem;
            margin-top: 20px;
        }

        .cta-btn {
            background-color: #FFD700;
            color: #111;
            font-weight: bold;
            padding: 10px 20px;
            border-radius: 5px;
            text-decoration: none;
            font-size: 1.2rem;
            margin-top: 20px;
        }

        .cta-btn:hover {
            background-color: #ffcc00;
            color: #222;
        }

        .info-section {
            background-color: #333;
            padding: 50px 0;
        }

        .info-section h2 {
            text-align: center;
            color: #FFD700;
            font-size: 2.5rem;
            margin-bottom: 30px;
        }

        .info-card {
            background-color: #444;
            padding: 30px;
            margin: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.5);
            text-align: center;
        }

        .info-card h3 {
            color: #FFD700;
            font-size: 1.8rem;
        }

        .info-card p {
            color: white;
            font-size: 1.2rem;
            margin-top: 10px;
        }

        footer {
            background-color: #111;
            color: white;
            padding: 20px;
            text-align: center;
        }

        footer a {
            color: #FFD700;
            text-decoration: none;
        }
        .carousel-section {
            background-color: #333;
            padding: 50px 0;
        }

        .carousel img {
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.7);
            max-height: 700px;
            object-fit: cover;
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

    <section class="hero-section">
        <div>
            <h1>Welcome to the Dungeon Master!</h1>
            <p>Explore dungeons, fight terrifying monsters, and conquer challenges in this roguelite FPS!</p>
            <a href="registerweb.php" class="cta-btn">Register now!</a>
        </div>
    </section>

    <section class="carousel-section py-5">
    <div class="container">
        <h2 class="text-center" style="color: #FFD700; font-size: 2.5rem; margin-bottom: 30px;">Explore the Adventure</h2>
        <div id="dungeonCarousel" class="carousel slide" data-bs-ride="carousel">
            <div class="carousel-indicators">
                <button type="button" data-bs-target="#dungeonCarousel" data-bs-slide-to="0" class="active"></button>
                <button type="button" data-bs-target="#dungeonCarousel" data-bs-slide-to="1"></button>
                <button type="button" data-bs-target="#dungeonCarousel" data-bs-slide-to="2"></button>
                <button type="button" data-bs-target="#dungeonCarousel" data-bs-slide-to="3"></button>
            </div>

            <div class="carousel-inner">
                <div class="carousel-item active">
                    <img src="images/carousel1.jpg" class="d-block w-100" alt="bemutato1">
                </div>
                <div class="carousel-item">
                    <img src="images/carousel2.jpg" class="d-block w-100" alt="bemutato2">
                </div>
                <div class="carousel-item">
                    <img src="images/carousel3.jpg" class="d-block w-100" alt="bemutato3">
                </div>
                <div class="carousel-item">
                    <img src="images/carousel4.jpg" class="d-block w-100" alt="bemutato4">
                </div>
            </div>

            <button class="carousel-control-prev" type="button" data-bs-target="#dungeonCarousel" data-bs-slide="prev">
                <span class="carousel-control-prev-icon"></span>
            </button>
            <button class="carousel-control-next" type="button" data-bs-target="#dungeonCarousel" data-bs-slide="next">
                <span class="carousel-control-next-icon"></span>
            </button>
        </div>
    </div>
</section>


    <section class="info-section">
        <div class="container">
            <h2>Game Features</h2>
            <div class="row">
                <div class="col-md-4">
                    <div class="info-card">
                        <h3>Endless Dungeons</h3>
                        <p>Delve into dungeons filled with enemies, traps, and treasures!</p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="info-card">
                        <h3>First-Person Combat</h3>
                        <p>Engage in intense first-person shooter combat as clear the dungeons of fierce monsters!</p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="info-card">
                        <h3>Roguelite Progression</h3>
                        <p>Upgrade your weapons, abilities, and gear with each run to become stronger and face tougher challenges!</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <footer>
        <p>&copy; 2025 Dungeon Master</p>
    </footer>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
