using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SymphonyAPI.Application.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace SymphonyAPI.Infrastructure.Security
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string HeaderName = "X-Api-Key"; // Header HTTP donde el cliente debe enviar la API Key.
        private readonly IApiKeyValidator _apiKeyValidator; // Servicio encargado de comprobar si la API Key recibida es válida.

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, UrlEncoder encoder,
            IApiKeyValidator apiKeyValidator) 
            : base(options, logger, encoder)
        {
            // Guardamos el validador para utilizarlo cuando llegue una petición HTTP.
            _apiKeyValidator = apiKeyValidator;
        }

        // Método que ASP.NET Core ejecuta para determinar si la petición está autenticada.
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Intentamos obtener el Header "X-Api-Key" de la petición HTTP.
            if (!Request.Headers.TryGetValue(HeaderName, out var providedApiKey))
            {
                // No se encontró la API Key.
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            // Enviamos la API Key recibida al servicio validador.
            // IApiKeyValidator se encarga de determinar si la clave proporcionada por el cliente es correcta.
            if (!_apiKeyValidator.Validate(providedApiKey.ToString()))
            {
                // La API Key existe, pero no es válida.
                // La autenticación falla y ASP.NET Core podrá devolver una respuesta 401 Unauthorized cuando corresponda.
                return Task.FromResult(AuthenticateResult.Fail("Invalid API-Key"));
            }

            // La API Key es válida.
            var claims = new[]
            {
                // Establecemos el nombre del cliente.
                // Posteriormente podremos acceder a este valor mediante: User.Identity?.Name
                new Claim(ClaimTypes.Name, "ApiKeyClient")
            };

            // Creamos una identidad a partir de los Claims.
            // Scheme.Name identifica el esquema de autenticación que está ejecutando este Handler.
            var identity = new ClaimsIdentity(claims, Scheme.Name);

            // Creamos el usuario autenticado utilizando la identidad.
            var principal = new ClaimsPrincipal(identity);

            // Creamos el AuthenticationTicket.
            // El ticket contiene la información que ASP.NET Core necesita para considerar autenticada la petición.
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            // Indicamos a ASP.NET Core que la autenticación se realizó correctamente.
            // A partir de aquí, un endpoint protegido con [Authorize] podrá reconocer al cliente como autenticado.
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
