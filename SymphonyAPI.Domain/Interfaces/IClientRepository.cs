using SymphonyAPI.Domain.Entities;

namespace SymphonyAPI.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task AddAsync(Client client);
        void Update(Client client);
        void Remove(Client client);
    }
}
