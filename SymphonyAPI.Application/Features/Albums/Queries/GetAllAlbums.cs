using MediatR;
using SymphonyAPI.Application.DTOs;
using SymphonyAPI.Application.Interfaces;

namespace SymphonyAPI.Application.Features.Albums.Queries
{
    public record GetAllAlbumsQuery : IRequest<IEnumerable<AlbumDTO>>;

    public class GetAllAlbumsHandler : IRequestHandler<GetAllAlbumsQuery, IEnumerable<AlbumDTO>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllAlbumsHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<AlbumDTO>> Handle(GetAllAlbumsQuery q, CancellationToken ct)
        {
            var albums = await _uow.Albums.GetAllAsync();
            return albums.Select(a => new AlbumDTO(a.Id, a.ArtistId, a.Title, a.CoverURL, a.Genre, a.ReleaseDate, a.Publisher));
        }
    }
}
