namespace SymphonyAPI.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation Properties
        public List<Order> Orders { get; } = new();
    }
}
