using SymphonyAPI.Application.Interfaces;
using SymphonyAPI.Domain.Interfaces;
using SymphonyAPI.Infrastructure.Persistence;

namespace SymphonyAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IArtistRepository Artists { get; }
        public IAlbumRepository Albums { get; }
        public IClientRepository Clients { get; }
        public IOrderRepository Orders { get; }
        public IOrderLineRepository OrderLines { get; }

        public UnitOfWork(AppDbContext context, IArtistRepository artists, IAlbumRepository albums, IClientRepository clients, IOrderRepository orders, IOrderLineRepository orderLines)
        {
            _context = context;
            Artists = artists;
            Albums = albums;
            Clients = clients;
            Orders = orders;
            OrderLines = orderLines;
        }

        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}
