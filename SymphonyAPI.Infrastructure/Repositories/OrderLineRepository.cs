using Microsoft.EntityFrameworkCore;
using SymphonyAPI.Domain.Entities;
using SymphonyAPI.Domain.Interfaces;
using SymphonyAPI.Infrastructure.Persistence;

namespace SymphonyAPI.Infrastructure.Repositories
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private readonly AppDbContext _context;
        public OrderLineRepository(AppDbContext context) => _context = context;

        public async Task<OrderLine?> GetByIdAsync(int id)
            => await _context.OrderLines.FindAsync(id);

        public async Task<IEnumerable<OrderLine>> GetAllAsync()
            => await _context.OrderLines.ToListAsync();

        public async Task AddAsync(OrderLine line)
            => await _context.OrderLines.AddAsync(line);

        public void Update(OrderLine line)
            => _context.OrderLines.Update(line);

        public void Remove(OrderLine line)
            => _context.OrderLines.Remove(line);
    }
}