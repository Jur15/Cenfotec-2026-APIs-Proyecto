using Microsoft.Extensions.Configuration;
using SymphonyAPI.Application.Security;

namespace SymphonyAPI.Infrastructure.Security
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        private readonly IConfiguration _configuration;

        public ApiKeyValidator(IConfiguration configuration) => _configuration = configuration;

        public bool Validate(string apiKey)
        {
            var expectedApiKey = _configuration["ApiKey:Value"];

            if (string.IsNullOrWhiteSpace(expectedApiKey))
                return false;
            return apiKey == expectedApiKey;
        }
    }
}
