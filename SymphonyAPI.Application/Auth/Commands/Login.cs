using MediatR;
using SymphonyAPI.Application.Interfaces;

namespace SymphonyAPI.Application.Auth.Commands
{
    public record LoginResponse(string Token);
    public record LoginCommand(string Username, string Password) : IRequest<LoginResponse>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(ITokenService tokenService) => _tokenService = tokenService;

        public Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Temporalmente, mientras no tienes repositorio
            if (request.Username != "admin" ||
                request.Password != "admin123")
            {
                throw new UnauthorizedAccessException();
            }

            var token = _tokenService.GenerateToken(
                userId: "1",
                role: "Admin");

            return Task.FromResult(
                new LoginResponse(token));
        }
    }
}
