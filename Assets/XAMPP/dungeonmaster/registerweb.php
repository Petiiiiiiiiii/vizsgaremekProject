<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Dungeon Master - Register</title>
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
    
    .container {
        max-width: 500px;
        background-color: #333;
        padding: 20px;
        border-radius: 10px;
        margin-top: 20px;
        margin-bottom: 20px;
    }
    
    .form-control {
        background-color: #444;
        color: white;
        border: none;
    }
    
    .btn-custom {
        background-color: #FFD700;
        color: #222;
        font-weight: bold;
    }
    #title{
       justify-content: center; 
    }
    </style>
    <script>
    function handleResponse(event) {
        event.preventDefault();
        
        var password = document.getElementById("password").value;
        var confirmPassword = document.getElementById("confirm_password").value;
        if (password !== confirmPassword) {
            alert("Passwords do not match");
            return;
        }

        var form = event.target;
        var formData = new FormData(form);

        fetch('register.php', {
            method: 'POST',
            body: formData
        })
        .then(response => response.text())
        .then(data => {
            if (data.trim() === "0") {
                alert("Successful registration! Redirecting to HomePage");
                setTimeout(() => {
                    window.location.href = 'index.php';
                }, 1000);
            } else {
                alert("Hiba történt: " + data);
            }
        })
        .catch(error => console.error('Hiba:', error));
    }
    </script>
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark">
        <div class="container" id="title">
            <a class="navbar-brand" href="index.php">Dungeon Master</a>
        </div>
    </nav>
    
    <div class="container">
        <h2 class="text-center">Register</h2>
        <form onsubmit="handleResponse(event)">
            <div class="mb-3">
                <label for="username" class="form-label">Username</label>
                <input type="text" class="form-control" id="username" name="username" required>
            </div>
            <div class="mb-3">
                <label for="email" class="form-label">Email</label>
                <input type="email" class="form-control" id="email" name="email" required>
            </div>
            <div class="mb-3">
                <label for="password" class="form-label">Password</label>
                <input type="password" class="form-control" id="password" name="pass" required>
            </div>
            <div class="mb-3">
                <label for="confirm_password" class="form-label">Confirm Password</label>
                <input type="password" class="form-control" id="confirm_password" required>
            </div>
            <button type="submit" class="btn btn-custom w-100">Register</button>
        </form>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
