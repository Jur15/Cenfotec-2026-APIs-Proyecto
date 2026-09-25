using MediatR;
using SymphonyAPI.Application.DTOs;
using SymphonyAPI.Application.Interfaces;
using SymphonyAPI.Domain.Exceptions;

namespace SymphonyAPI.Application.Features.Albums.Queries
{
    public record GetAlbumByIdQuery(int Id) : IRequest<AlbumDTO?>;

    public class GetAlbumByIdHandler : IRequestHandler<GetAlbumByIdQuery, AlbumDTO?>
    {
        private readonly IUnitOfWork _uow;
        public GetAlbumByIdHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<AlbumDTO?> Handle(GetAlbumByIdQuery q, CancellationToken ct)
        {
            var a = await _uow.Albums.GetByIdAsync(q.Id)
                ?? throw new NotFoundException($"Can't find an Album with the ID {q.Id}.");
            return new AlbumDTO(a.Id, a.ArtistId, a.Title, a.CoverURL, a.Genre, a.ReleaseDate, a.Publisher);
        }
    }
}
