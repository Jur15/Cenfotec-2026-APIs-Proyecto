using MediatR;
using SymphonyAPI.Application.DTOs;
using SymphonyAPI.Application.Interfaces;
using SymphonyAPI.Domain.Exceptions;

namespace SymphonyAPI.Application.Features.Artists.Queries
{
    public record GetArtistByIdQuery(int Id) : IRequest<ArtistDTO?>;

    public class GetArtistByIdHandler : IRequestHandler<GetArtistByIdQuery, ArtistDTO?>
    {
        private readonly IUnitOfWork _uow;
        public GetArtistByIdHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<ArtistDTO?> Handle(GetArtistByIdQuery q, CancellationToken ct)
        {
            var a = await _uow.Artists.GetByIdAsync(q.Id) 
                ?? throw new NotFoundException($"Can't find an Artist with the ID {q.Id}.");
            return new ArtistDTO(a.Id, a.Name, a.PhotoURL, a.Bio);
        }
    }
}
