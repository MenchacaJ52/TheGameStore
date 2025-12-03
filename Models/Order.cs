using System.Reflection;

namespace GameStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; set; } = "";
        public int ListingId { get; set; }
        public Listing Listing { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";

    }
}
