using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        // Optional Identity user id (if using Identity)
        public string? UserId { get; set; }

        // Optional link to local Account record
        public int? AccountId { get; set; }
        public Account? Account { get; set; }

        [Required, StringLength(100)]
        public string CardholderName { get; set; } = string.Empty;

        // DO NOT store full card numbers in production. This example stores a token/last4 only.
        [Required, StringLength(4)]
        public string Last4 { get; set; } = string.Empty;

        [Range(1, 12)]
        public int ExpiryMonth { get; set; }

        [Range(2000, 2100)]
        public int ExpiryYear { get; set; }

        public bool IsDefault { get; set; } = false;
    }
}