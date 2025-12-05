using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public enum GameCondition
    {
        New = 0,
        LikeNew = 1,
        Good = 2,
        Fair = 3,
        Poor = 4
    }

    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Platform { get; set; } = string.Empty;

        [Range(0.01, 9999)]
        public decimal Price { get; set; }

        [Required]
        public GameCondition Condition { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required, StringLength(100)]
        public string Genre { get; set; } = string.Empty;

        [Required, StringLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}

