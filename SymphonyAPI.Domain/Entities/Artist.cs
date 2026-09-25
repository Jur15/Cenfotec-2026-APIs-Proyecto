namespace SymphonyAPI.Domain.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhotoURL { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;

        // Navigation Properties
        public List<Album> Albums { get; } = new();
    }
}
