using MediatR;
using SymphonyAPI.Application.Interfaces;

namespace SymphonyAPI.Application.Features.Artists.Commands
{
    public record UpdateArtistCommand(int Id, string Name, string PhotoURL, string Bio) : IRequest<bool>;
    
    public class UpdateArtistHandler : IRequestHandler<UpdateArtistCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        public UpdateArtistHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<bool> Handle(UpdateArtistCommand cmd, CancellationToken ct)
        {
            var artist = await _uow.Artists.GetByIdAsync(cmd.Id);
            if (artist == null) return false;

            artist.Name = cmd.Name;
            artist.PhotoURL = cmd.PhotoURL;
            artist.Bio = cmd.Bio;

            _uow.Artists.Update(artist);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
