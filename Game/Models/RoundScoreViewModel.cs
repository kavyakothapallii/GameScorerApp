using System.Collections.Generic;

namespace Game.Models
{
    public class RoundScoreViewModel
    {
        public int GameId { get; set; }                  
        public List<Player> Players { get; set; }       
        public int MaxScore { get; set; }                
    }
}
