using SymphonyAPI.Domain.Entities;

namespace SymphonyAPI.Domain.Interfaces
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetAllAsync();
        Task<Album?> GetByIdAsync(int id);
        Task AddAsync(Album album);
        void Update(Album album);
        void Remove(Album album);
    }
}
