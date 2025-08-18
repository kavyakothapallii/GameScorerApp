namespace Game.Models
{
    public class Games
    {

        public int Id { get; set; }
        public int MaxScore { get; set; }
        public DateTime CreatedDate {  get; set; }
        public ICollection<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();
        public ICollection<GameRound> GameRounds { get; set; } = new List<GameRound>();

    }
}
