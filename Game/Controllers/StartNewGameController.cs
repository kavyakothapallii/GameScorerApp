using Microsoft.AspNetCore.Mvc;
using Game.Data;
using Game.Models;
using System.Linq;
using System.Collections.Generic;

namespace Game.Controllers
{
    public class StartNewGameController : Controller
    {
        private readonly AppDbContext _db;
        public StartNewGameController(AppDbContext db)
        {
            _db = db;
        }

        // Page 1 - list of players
        public IActionResult Index()
        {
            var list = _db.Players.ToList();
            return View(list);
        }

        // Page 2 - GET: show selection form
        [HttpGet]
        public IActionResult Select()
        {
            var players = _db.Players.ToList();
            return View(players);
        }

        // Page 2 - POST: submit selected players and max score
        [HttpPost]
        public IActionResult Select(int maxscore, int[] selected)
        {
            // Validation
            if (selected == null || selected.Length < 2)
            {
                ModelState.AddModelError("", "Minimum of 2 players needed");
                return View(_db.Players.ToList());
            }

            if (maxscore <= 0)
            {
                ModelState.AddModelError("", "Maximum Score has to be greater than 0.");
                return View(_db.Players.ToList());
            }

            // Create new game
            var game = new Games
            {
                MaxScore = maxscore,
                CreatedDate = DateTime.Now,
                GamePlayers = selected.Select(playerId => new GamePlayer
                {
                    PlayerId = playerId,
                    TotalScore = 0,
                    IsOut = false
                }).ToList()
            };

            // Add game (EF Core automatically sets GameId in GamePlayers)
            _db.Games.Add(game);
            _db.SaveChanges();

            // Redirect to GameRounds page with the new game's ID
            return RedirectToAction("Index", "Play", new { gameId = game.Id });
        }
    }
}
