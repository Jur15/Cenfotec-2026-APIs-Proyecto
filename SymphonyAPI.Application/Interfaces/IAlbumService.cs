using SymphonyAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymphonyAPI.Application.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDTO>> GetAllAsync();
        Task<AlbumDTO?> GetByIdAsync(int id);
        Task<AlbumDTO> CreateAsync(CreateAlbumDTO dto);
        Task<bool> UpdateAsync(int id, UpdateAlbumDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
