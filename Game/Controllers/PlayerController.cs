using Game.Data;
using Game.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Game.Controllers
{
    public class PlayerController : Controller
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Player List
        public IActionResult Index()
        {
            var players = _context.Players.OrderBy(p => p.Name).ToList();
            return View(players);
        }

        // GET: Add Player
        public IActionResult Add()
        {
            return View();
        }

        // POST: Add Player
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Player player)
        {
            if (_context.Players.Count() >= 10)
            {
                return BadRequest("Max limit is 10 players.");
            }
            if (ModelState.IsValid)
            {
                
                _context.Players.Add(player);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Delete Player
        public IActionResult Delete(int id)
        {
            var player = _context.Players.Find(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
