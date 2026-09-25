using SymphonyAPI.Domain.Entities;

namespace SymphonyAPI.Domain.Interfaces
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist?> GetByIdAsync(int id);
        Task AddAsync(Artist artist);
        void Update(Artist artist);
        void Remove(Artist artist);
    }
}
