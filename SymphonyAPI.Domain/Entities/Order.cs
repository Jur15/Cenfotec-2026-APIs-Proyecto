namespace SymphonyAPI.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; } // Foreign Key to Client
        public DateTime Date { get; set; }

        // Navigation properties
        public Client Client { get; set; } = null!;
        public List<OrderLine> OrderLines { get; } = new();
    }
}
