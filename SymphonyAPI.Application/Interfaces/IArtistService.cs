using SymphonyAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymphonyAPI.Application.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistDTO>> GetAllAsync();
        Task<ArtistDTO?> GetByIdAsync(int id);
        Task<ArtistDTO> CreateAsync(CreateArtistDTO dto);
        Task<bool> UpdateAsync(int id, UpdateArtistDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
