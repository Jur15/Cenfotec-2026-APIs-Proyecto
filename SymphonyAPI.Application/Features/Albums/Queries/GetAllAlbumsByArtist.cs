using MediatR;
using SymphonyAPI.Application.DTOs;
using SymphonyAPI.Application.Interfaces;

namespace SymphonyAPI.Application.Features.Albums.Queries
{
    public record GetAllAlbumsByArtistQuery(int ArtistId) : IRequest<IEnumerable<AlbumDTO>>;

    public class GetAllAlbumsByArtistHandler : IRequestHandler<GetAllAlbumsByArtistQuery, IEnumerable<AlbumDTO>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllAlbumsByArtistHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<AlbumDTO>> Handle(GetAllAlbumsByArtistQuery q, CancellationToken ct)
        {
            var albums = await _uow.Albums.GetAllAsync();
            var filtered = albums.Where(a => a.ArtistId == q.ArtistId);
            return filtered.Select(a => new AlbumDTO(a.Id, a.ArtistId, a.Title, a.CoverURL, a.Genre, a.ReleaseDate, a.Publisher));
        }
    }
}
