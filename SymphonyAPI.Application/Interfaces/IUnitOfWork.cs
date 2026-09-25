using SymphonyAPI.Domain.Interfaces;

namespace SymphonyAPI.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IArtistRepository Artists { get; }
        IAlbumRepository Albums { get; }
        IClientRepository Clients { get; }
        IOrderRepository Orders { get; }
        IOrderLineRepository OrderLines { get; }

        Task<int> SaveChangesAsync();
    }
}
