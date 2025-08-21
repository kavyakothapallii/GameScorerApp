using Microsoft.EntityFrameworkCore;
using Game.Models;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship: A Game can have one Winner (Player)
            modelBuilder.Entity<Games>()
                .HasOne(g => g.Winner)
                .WithMany()
                .HasForeignKey(g => g.WinnerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete
        }
    }
}
