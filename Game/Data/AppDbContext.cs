using Microsoft.EntityFrameworkCore;
using Game.Models;
using Microsoft.Identity.Client;
namespace Game.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
    }
        public DbSet<Player> Players { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }
        public DbSet<GameRound> GameRounds { get; set; }         
        public DbSet<GamePlayerScore> GamePlayerScores { get; set; }







    }

}
