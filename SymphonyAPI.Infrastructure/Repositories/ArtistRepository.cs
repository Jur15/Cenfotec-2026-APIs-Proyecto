using Microsoft.EntityFrameworkCore;
using SymphonyAPI.Domain.Entities;
using SymphonyAPI.Domain.Interfaces;
using SymphonyAPI.Infrastructure.Persistence;

namespace SymphonyAPI.Infrastructure.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _context;
        public ArtistRepository(AppDbContext context) => _context = context;

        public async Task<Artist?> GetByIdAsync(int id)
            => await _context.Artists.FindAsync(id);

        public async Task<IEnumerable<Artist>> GetAllAsync()
            => await _context.Artists.ToListAsync();

        public async Task AddAsync(Artist artist)
            => await _context.Artists.AddAsync(artist);

        public void Update(Artist artist)
            => _context.Artists.Update(artist);

        public void Remove(Artist artist)
            => _context.Artists.Remove(artist);
    }
}
