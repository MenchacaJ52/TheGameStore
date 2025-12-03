namespace GameStore.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
        public string SellerId { get; set; } = ""; 
        public bool IsActive { get; set; } = true;
        public int Quantity { get; set; } = 1;
        public decimal AskingPrice { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.UtcNow;

    }
}
