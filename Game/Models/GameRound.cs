using Game.Models;

public class GameRound
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int RoundNumber { get; set; }


    public List<GamePlayerScore> GamePlayerScores { get; set; } = new List<GamePlayerScore>();

    public Games Game { get; set; }
}
