using Game.Data;
using Game.Models;
using Game.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Game.Controllers
{
    public class PlayController : Controller
    {
        private readonly AppDbContext _db;

        public PlayController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /Play/Index/{gameId}
        public IActionResult Index(int gameId)
        {
            var game = _db.Games
                .Include(g => g.GamePlayers)
                .ThenInclude(gp => gp.Player)
                .FirstOrDefault(g => g.Id == gameId);

            if (game == null) return NotFound();

            var model = new RoundScoreViewModel
            {
                GameId = game.Id,
                Players = game.GamePlayers.Select(gp => gp.Player).ToList(),
                MaxScore = game.MaxScore
            };

            return View(model);
        }

        // GET API: /api/play/{gameId}
        [HttpGet("api/play/{gameId}")]
        public IActionResult GetGame(int gameId)
        {
            var game = _db.Games
                .Include(g => g.GamePlayers)
                .ThenInclude(gp => gp.Player)
                .Include(g => g.GameRounds)
                .ThenInclude(r => r.GamePlayerScores)
                .FirstOrDefault(g => g.Id == gameId);

            if (game == null) return NotFound();

            var players = game.GamePlayers.Select(gp => new
            {
                gp.PlayerId,
                gp.Player.Name,
                gp.TotalScore,
                gp.IsOut
            }).ToList();

            var rounds = game.GameRounds.Select(r => new
            {
                r.RoundNumber,
                Scores = r.GamePlayerScores.Select(s => new { s.PlayerId, s.Score })
            }).ToList();

            return Ok(new
            {
                GameId = game.Id,
                MaxScore = game.MaxScore,
                CurrentRound = game.GameRounds.Count + 1,
                Players = players,
                Rounds = rounds
            });
        }

        // POST API: /api/play/{gameId}/round
        [HttpPost("api/play/{gameId}/round")]
        public IActionResult SubmitRound(int gameId, [FromBody] RoundScoreDTO roundScores)
        {
            var game = _db.Games
                .Include(g => g.GamePlayers)
                .Include(g => g.GameRounds)
                .FirstOrDefault(g => g.Id == gameId);

            if (game == null) return NotFound();

            // Create new round
            var round = new GameRound
            {
                GameId = game.Id,
                RoundNumber = game.GameRounds.Count + 1
            };
            _db.GameRounds.Add(round);
            _db.SaveChanges();

            // Update player scores
            foreach (var score in roundScores.Scores)
            {
                var gp = game.GamePlayers.FirstOrDefault(x => x.PlayerId == score.PlayerId);
                if (gp == null) continue;

                gp.TotalScore += score.Score;
                if (gp.TotalScore >= game.MaxScore)
                    gp.IsOut = true;

                var roundScore = new GamePlayerScore
                {
                    GameRoundId = round.Id,
                    PlayerId = gp.PlayerId,
                    Score = score.Score
                };
                _db.GamePlayerScores.Add(roundScore);
            }

            _db.SaveChanges();

            // Return updated totals
            return Ok(new
            {
                Message = "Round submitted",
                Players = game.GamePlayers.Select(gp => new {
                    gp.PlayerId,
                    gp.TotalScore,
                    gp.IsOut
                })
            });
        }
    }
}
