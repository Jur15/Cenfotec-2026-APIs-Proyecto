using Microsoft.EntityFrameworkCore;
using SymphonyAPI.Domain.Entities;
using SymphonyAPI.Domain.Interfaces;
using SymphonyAPI.Infrastructure.Persistence;

namespace SymphonyAPI.Infrastructure.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AppDbContext _context;
        public AlbumRepository(AppDbContext context) => _context = context;

        public async Task<Album?> GetByIdAsync(int id)
            => await _context.Albums.FindAsync(id);

        public async Task<IEnumerable<Album>> GetAllAsync()
            => await _context.Albums.ToListAsync();

        public async Task AddAsync(Album album)
            => await _context.Albums.AddAsync(album);

        public void Update(Album album)
            => _context.Albums.Update(album);

        public void Remove(Album album)
            => _context.Albums.Remove(album);
    }
}
