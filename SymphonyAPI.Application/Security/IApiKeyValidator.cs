namespace SymphonyAPI.Application.Security
{
    public interface IApiKeyValidator
    {
        bool Validate(string apiKey);
    }
}
