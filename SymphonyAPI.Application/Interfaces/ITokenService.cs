namespace SymphonyAPI.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string role);
    }
}
