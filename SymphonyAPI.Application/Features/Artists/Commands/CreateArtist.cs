using MediatR;
using SymphonyAPI.Application.Interfaces;
using SymphonyAPI.Domain.Entities;

namespace SymphonyAPI.Application.Features.Artists.Commands
{
    public record CreateArtistCommand(string Name, string PhotoURL, string Bio) : IRequest<int>;
    
    public class CreateArtistHandler : IRequestHandler<CreateArtistCommand, int>
    {
        private readonly IUnitOfWork _uow;
        public CreateArtistHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<int> Handle(CreateArtistCommand cmd, CancellationToken ct)
        {
            var artist = new Artist
            {
                Name = cmd.Name,
                PhotoURL = cmd.PhotoURL,
                Bio = cmd.Bio
            };
            await _uow.Artists.AddAsync(artist);
            await _uow.SaveChangesAsync();
            return artist.Id;
        }
    }
}
