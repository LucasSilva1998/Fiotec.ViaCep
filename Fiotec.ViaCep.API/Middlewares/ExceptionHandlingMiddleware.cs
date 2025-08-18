using Fiotec.ViaCep.Application.Exceptions;
using System.Net;

namespace Fiotec.ViaCep.API.Middlewares
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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ParametroInvalidoException ex)
            {
                _logger.LogWarning(ex, "Parâmetro inválido.");
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (NaoEncontradoException ex)
            {
                _logger.LogWarning(ex, "Recurso não encontrado.");
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno no servidor.");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Ocorreu um erro interno.");
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            var response = new
            {
                StatusCode = (int)statusCode,
                Message = message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var json = System.Text.Json.JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
