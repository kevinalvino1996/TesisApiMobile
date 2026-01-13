using FluentValidation;
using MobilePedidos.Shared.Errors;
using MobilePedidos.Shared.Responses;
using System.Net;
using System.Text.Json;

namespace MobilePedidos.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Error de validación en {Path}", context.Request.Path);
                
                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .Select(g => new ValidationError
                    {
                        Field = g.Key,
                        Errors = g.Select(x => x.ErrorMessage).ToList()
                    })
                    .ToList();

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var response = new GeneralResponse<List<ValidationError>>
                {
                    Success = false,
                    ShowAlert = true,
                    Title = "Error de validación",
                    Message = "Datos inválidos",
                    Data = errors
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado en {Path}: {Message}", context.Request.Path, ex.Message);
                
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new GeneralResponse<string>
                {
                    Success = false,
                    ShowAlert = true,
                    Title = "Error interno",
                    Message = "Ha ocurrido un error inesperado. Por favor, contacte al administrador."
                });
            }
        }
    }
}
