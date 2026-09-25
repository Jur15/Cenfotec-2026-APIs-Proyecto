using Microsoft.EntityFrameworkCore;
using SymphonyAPI.Domain.Entities;
using SymphonyAPI.Domain.Interfaces;
using SymphonyAPI.Infrastructure.Persistence;

namespace SymphonyAPI.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) => _context = context;

        public async Task<Order?> GetByIdAsync(int id)
            => await _context.Orders.FindAsync(id);

        public async Task<IEnumerable<Order>> GetAllAsync()
            => await _context.Orders.ToListAsync();

        public async Task AddAsync(Order order)
            => await _context.Orders.AddAsync(order);

        public void Update(Order order)
            => _context.Orders.Update(order);

        public void Remove(Order order)
            => _context.Orders.Remove(order);
    }
}
