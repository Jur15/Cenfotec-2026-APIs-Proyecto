using Microsoft.AspNetCore.Mvc;
using SymphonyAPI.Application.Exceptions;
using SymphonyAPI.Domain.Exceptions;
using System.Text.Json;

namespace SymphonyAPI.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext ctx)
        {
            try { await _next(ctx); }
            catch (ValidationException ex)
            {
                await Write(ctx, 400, "Validación", ex.Message);
            }
            catch (NotFoundException ex)
            {
                await Write(ctx, 404, "No encontrado", ex.Message);
            }
            catch (Exception)
            {
                await Write(ctx, 500, "Error interno",
                    "Ocurrió un error inesperado");
            }
        }

        private static Task Write(
            HttpContext ctx, int status, string title, string detail)
        {
            ctx.Response.ContentType = "application/problem+json";
            ctx.Response.StatusCode = status;
            var problem = new ProblemDetails
            {
                Type = $"https://httpstatuses.com/{status}",
                Title = title,
                Status = status,
                Detail = detail,
                Instance = ctx.Request.Path
            };
            return ctx.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}