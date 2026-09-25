using System;
using System.Collections.Generic;
using System.Text;

namespace SymphonyAPI.Application.DTOs
{
    public record ArtistDTO(int Id, string Name, string PhotoURL, string Bio);
    public record CreateArtistDTO(string Name, string PhotoURL, string Bio);
    public record UpdateArtistDTO(string Name, string PhotoURL, string Bio);
}
