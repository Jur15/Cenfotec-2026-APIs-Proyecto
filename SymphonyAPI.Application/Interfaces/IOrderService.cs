using SymphonyAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymphonyAPI.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllAsync();
        Task<OrderDTO?> GetByIdAsync(int id);
        Task<OrderDTO> CreateAsync(CreateOrderDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
