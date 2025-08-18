using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game.Models
{
    public class GamePlayerScore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int GameRoundId { get; set; }
        public GameRound GameRound { get; set; }

        public int PlayerId { get; set; }

        public int Score { get; set; }
    }
}
