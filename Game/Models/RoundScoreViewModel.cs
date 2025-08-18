using System.Collections.Generic;

namespace Game.Models
{
    public class RoundScoreViewModel
    {
        public int GameId { get; set; }                  // ID of the current game
        public List<Player> Players { get; set; }        // List of players in the game
        public int MaxScore { get; set; }                // Max score limit for the game
    }
}
