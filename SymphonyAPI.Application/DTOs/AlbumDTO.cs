using System;
using System.Collections.Generic;
using System.Text;

namespace SymphonyAPI.Application.DTOs
{
    public record AlbumDTO(int Id, int ArtistId, string Title, string CoverURL, string Genre, DateOnly ReleaseDate, string Publisher);
    public record CreateAlbumDTO(int ArtistId, string Title, string CoverURL, string Genre, DateOnly ReleaseDate, string Publisher);
    public record UpdateAlbumDTO(int ArtistId, string Title, string CoverURL, string Genre, DateOnly ReleaseDate, string Publisher);
}
