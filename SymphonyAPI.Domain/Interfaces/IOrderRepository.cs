using SymphonyAPI.Domain.Entities;

namespace SymphonyAPI.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
        void Update(Order order);
        void Remove(Order order);
    }
}
