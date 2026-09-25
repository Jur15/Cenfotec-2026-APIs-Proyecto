using MediatR;
using SymphonyAPI.Application.DTOs;
using SymphonyAPI.Application.Interfaces;

namespace SymphonyAPI.Application.Features.Artists.Queries
{
    public record GetAllArtistsQuery : IRequest<IEnumerable<ArtistDTO>>;
    
    public class GetAllArtistsHandler : IRequestHandler<GetAllArtistsQuery, IEnumerable<ArtistDTO>>
    {
        private readonly IUnitOfWork _uow;
        public GetAllArtistsHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<ArtistDTO>> Handle(GetAllArtistsQuery q, CancellationToken ct)
        {
            var artists = await _uow.Artists.GetAllAsync();
            return artists.Select(a => new ArtistDTO(a.Id, a.Name, a.PhotoURL, a.Bio));
        }
    }
}
