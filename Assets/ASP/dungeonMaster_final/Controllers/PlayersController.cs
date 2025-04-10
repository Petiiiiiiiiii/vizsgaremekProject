﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dungeonMaster_final.Models;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace dungeonMaster_final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly DungeonMaster _context;

        public PlayersController(DungeonMaster context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players
                .Include(p => p.MatchLogs)
                .Include(p => p.UnlockedSkills)
                .ToListAsync();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players
                .Include(p => p.MatchLogs)
                .Include(p => p.UnlockedSkills)
                .FirstOrDefaultAsync(p => p.PlayerId == id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Players/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Players
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> PostPlayer(PlayerRegistrationDto registrationDto)
        {
            try
            {
                // Ellenőrzések
                if (await _context.Players.AnyAsync(p => p.Username == registrationDto.Username))
                    return Conflict(new ApiResponse { Code = 3, Message = "Username already exists" });

                if (await _context.Players.AnyAsync(p => p.Email == registrationDto.Email))
                    return Conflict(new ApiResponse { Code = 5, Message = "Email already exists" });

                // Játékos létrehozása
                var player = new Player
                {
                    Username = registrationDto.Username,
                    Email = registrationDto.Email,
                    PasswordHash = HashPassword(registrationDto.Password)
                };

                _context.Players.Add(player);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse { Success = true });
            }
            catch (DbUpdateException ex)
            {
                // Adatbázis szintű egyediségi hiba
                var sqlEx = ex.InnerException as SqlException;
                if (sqlEx != null && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    var errorMessage = sqlEx.Message.Contains("Username")
                        ? new ApiResponse { Code = 3, Message = "Username already exists" }
                        : new ApiResponse { Code = 5, Message = "Email already exists" };

                    return Conflict(errorMessage);
                }
                return StatusCode(500, new ApiResponse { Message = "Database error" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = "Internal server error" });
            }
        }

        public class ApiResponse
        {
            public bool Success { get; set; }
            public int Code { get; set; }
            public string Message { get; set; }
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<Player>> Login([FromBody] LoginDto loginDto)
        {
            var player = await _context.Players
                .FirstOrDefaultAsync(p => p.Username == loginDto.Username);

            if (player == null)
                return Unauthorized(new { message = "Hibás felhasználónév vagy jelszó" });

            var inputHash = HashPassword(loginDto.PasswordHash);

            if (player.PasswordHash != inputHash)
                return Unauthorized(new { message = "Hibás felhasználónév vagy jelszó" });

            // Ne küldjük vissza a jelszó hash-t
            player.PasswordHash = null;

            return Ok(player);
        }

        public class LoginDto
        {
            public string Username { get; set; }
            public string PasswordHash { get; set; }
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        [HttpGet("scoreboard")]
        public async Task<ActionResult<IEnumerable<ScoreboardDto>>> GetScoreboard()
        {
            return await _context.Players
                .Select(p => new ScoreboardDto
                {
                    Username = p.Username,
                    Kills = p.MatchLogs.Sum(m => m.Kills),
                    Deaths = p.MatchLogs.Count,
                    Level = p.Level,
                    Playtime = p.MatchLogs.Sum(m => m.MatchDuration),
                    RegDate = p.RegDate
                })
                .OrderByDescending(p => p.Level)
                .ThenByDescending(p => p.Kills)
                .ToListAsync();
        }

        public class ScoreboardDto
        {
            public string Username { get; set; }
            public int Kills { get; set; }
            public int Deaths { get; set; }
            public int Level { get; set; }
            public int Playtime { get; set; }
            public DateTime RegDate { get; set; }
        }
    }
    public class PlayerCredentials
    {
        public string username { get; set; } = null!;
        public string passwordHash { get; set; } = null!;
    }
}