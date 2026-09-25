using MediatR;
using SymphonyAPI.Application.Interfaces;

namespace SymphonyAPI.Application.Features.Albums.Commands
{
    public record DeleteAlbumCommand(int Id) : IRequest<bool>;
    
    public class DeleteAlbumHandler : IRequestHandler<DeleteAlbumCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        public DeleteAlbumHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<bool> Handle(DeleteAlbumCommand cmd, CancellationToken ct)
        {
            var album = await _uow.Albums.GetByIdAsync(cmd.Id);
            if (album == null) return false;

            _uow.Albums.Remove(album);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
