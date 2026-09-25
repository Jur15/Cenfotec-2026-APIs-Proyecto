using MediatR;
using SymphonyAPI.Application.Interfaces;

namespace SymphonyAPI.Application.Features.Artists.Commands
{
    public record DeleteArtistCommand(int Id) : IRequest<bool>;
    
    public class DeleteArtistHandler : IRequestHandler<DeleteArtistCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        public DeleteArtistHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<bool> Handle(DeleteArtistCommand cmd, CancellationToken ct)
        {
            var artist = await _uow.Artists.GetByIdAsync(cmd.Id);
            if (artist == null) return false;

            _uow.Artists.Remove(artist);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
