using SymphonyAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymphonyAPI.Application.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDTO>> GetAllAsync();
        Task<ClientDTO?> GetByIdAsync(int id);
        Task<ClientDTO> CreateAsync(CreateClientDTO dto);
        Task<bool> UpdateAsync(int id, UpdateClientDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
