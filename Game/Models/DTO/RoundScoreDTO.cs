namespace Game.Models.DTO
{
    public class RoundScoreDTO
    {
        public List<PlayerScoreDTO> Scores { get; set; } = new List<PlayerScoreDTO>();
    }

    public class PlayerScoreDTO
    {
        public int PlayerId { get; set; }   
        public int Score { get; set; }      
    }
}
