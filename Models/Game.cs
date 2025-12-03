using GameStore.Services;
using System.Net.NetworkInformation;

namespace GameStore.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Platform { get; set; } = "";
        public string Genre { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal VeryGoodPrice => OriginalPrice * 0.75m;
        public decimal GoodPrice => OriginalPrice * 0.65m;
        public decimal FairPrice => OriginalPrice * 0.50m;
        public decimal AcceptablePrice => OriginalPrice * 0.30m;

        public decimal StoreCreditValue => Price * 0.40m;
        public decimal CashValue => Price * 0.25m;
        public string CoverImageUrl { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
