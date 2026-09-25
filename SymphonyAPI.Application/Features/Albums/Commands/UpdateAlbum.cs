using MediatR;
using SymphonyAPI.Application.Interfaces;

namespace SymphonyAPI.Application.Features.Albums.Commands
{
    public record UpdateAlbumCommand(int Id, int ArtistId, string Title, string CoverURL,
        string Genre, DateOnly ReleaseDate, string Publisher) : IRequest<bool>;

    public class UpdateAlbumHandler : IRequestHandler<UpdateAlbumCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        public UpdateAlbumHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<bool> Handle(UpdateAlbumCommand cmd, CancellationToken ct)
        {
            var a = await _uow.Albums.GetByIdAsync(cmd.Id);
            if (a == null) return false;

            a.ArtistId = cmd.ArtistId; 
            a.Title = cmd.Title; 
            a.CoverURL = cmd.CoverURL; 
            a.Genre = cmd.Genre; 
            a.ReleaseDate = cmd.ReleaseDate; 
            a.Publisher = cmd.Publisher;

            _uow.Albums.Update(a);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
