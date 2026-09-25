namespace SymphonyAPI.Domain.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public int ArtistId { get; set; } // Foreign Key to Artist
        public string Title { get; set; } = string.Empty;
        public string CoverURL { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateOnly ReleaseDate { get; set; }
        public string Publisher { get; set; } = string.Empty;

        // Navigation Properties
        public Artist Artist { get; set; } = null!; 
        public List<OrderLine> OrderLines { get; } = new();
    }
}
