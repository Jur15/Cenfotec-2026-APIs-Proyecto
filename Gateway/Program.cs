using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.QualityOfService.Polly;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// JWT
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
var jwtAudience = builder.Configuration["Jwt:Audience"]!;

builder.Services.AddAuthentication()
       .AddJwtBearer("Bearer", options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,

               ValidIssuer = jwtIssuer,
               ValidAudience = jwtAudience,

               IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
           };
       });

builder.Services.AddAuthorization();

// Caching + Rate limiting
builder.Services
       .AddOcelot(builder.Configuration)
       .AddCacheManager(x => x.WithDictionaryHandle())
       .AddPolly();                 // reintentos / timeout

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();   // ← middleware principal, siempre al final

app.Run();