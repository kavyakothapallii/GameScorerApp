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


        public IActionResult Index()
        {
            var list = _db.Players.ToList();
            return View(list);
        }


        [HttpGet]
        public IActionResult Select()
        {
            var players = _db.Players.ToList();
            return View(players);
        }

 
        [HttpPost]
        public IActionResult Select(int maxscore, int[] selected)
        {
    
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

    
            _db.Games.Add(game);
            _db.SaveChanges();

  
            return RedirectToAction("Index", "Play", new { gameId = game.Id });
        }
    }
}
