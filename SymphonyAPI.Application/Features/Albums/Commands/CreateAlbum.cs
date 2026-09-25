using MediatR;
using SymphonyAPI.Application.Interfaces;
using SymphonyAPI.Domain.Entities;

namespace SymphonyAPI.Application.Features.Albums.Commands
{
    public record CreateAlbumCommand(int ArtistId, string Title, string CoverURL, 
        string Genre, DateOnly ReleaseDate, string Publisher) : IRequest<int>;

    public class CreateAlbumHandler : IRequestHandler<CreateAlbumCommand, int>
    {
        private readonly IUnitOfWork _uow;

        public CreateAlbumHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<int> Handle(CreateAlbumCommand cmd, CancellationToken ct)
        {
            var album = new Album
            {
                ArtistId = cmd.ArtistId,
                Title = cmd.Title,
                CoverURL = cmd.CoverURL, 
                Genre = cmd.Genre, 
                ReleaseDate = cmd.ReleaseDate, 
                Publisher = cmd.Publisher
            };
            await _uow.Albums.AddAsync(album);
            await _uow.SaveChangesAsync();
            return album.Id;
        }
    }
}
