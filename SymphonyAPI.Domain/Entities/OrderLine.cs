namespace SymphonyAPI.Domain.Entities
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Foreign Key to Order
        public int AlbumId { get; set; } // Foreign Key to Album
        public int Quantity { get; set; }

        // Navigation properties
        public Order Order { get; } = null!;
        public Album Album { get; } = null!;
    }
}
