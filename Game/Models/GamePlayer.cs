namespace Game.Models
{
    public class GamePlayer
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Games Game { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int TotalScore { get; set; } = 0;
        public bool IsOut { get; set; } = false;
    }
}
