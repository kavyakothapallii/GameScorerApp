namespace Game.Models
{
    public class Games
    {
        public int Id { get; set; }
        public int MaxScore { get; set; }
        public DateTime CreatedDate { get; set; }

        // NEW: Winner tracking
        public int? WinnerId { get; set; }   // Nullable since game may not be finished yet
        public Player? Winner { get; set; }  // Navigation property to Player

        public ICollection<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();
        public ICollection<GameRound> GameRounds { get; set; } = new List<GameRound>();
    }
}
