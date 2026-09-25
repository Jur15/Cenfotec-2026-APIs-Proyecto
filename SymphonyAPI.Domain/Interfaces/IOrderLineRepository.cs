using SymphonyAPI.Domain.Entities;

namespace SymphonyAPI.Domain.Interfaces
{
    public interface IOrderLineRepository
    {
        Task<IEnumerable<OrderLine>> GetAllAsync();
        Task<OrderLine?> GetByIdAsync(int id);
        Task AddAsync(OrderLine order);
        void Update(OrderLine order);
        void Remove(OrderLine order);
    }
}
