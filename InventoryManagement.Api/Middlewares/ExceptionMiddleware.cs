using FluentValidation;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Domain.Exceptions;
using System.Net;

namespace InventoryManagement.Api.Middlewares
{
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;

            if (path != null && path.StartsWith("/swagger"))
            {
                await _next(context);
                return;
            }

            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Error de validación en la ruta {Path}", context.Request.Path);

                await HandleExceptionAsync(context, HttpStatusCode.BadRequest,ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                _logger.LogWarning(ex, "Acceso no autorizado en la ruta {Path}",context.Request.Path);

                await HandleExceptionAsync( context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (ConflictException ex)
            {
                _logger.LogWarning(ex, "Conflicto de negocio en la ruta {Path}", context.Request.Path);

                await HandleExceptionAsync(context, HttpStatusCode.Conflict, ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning( ex, "Recurso no encontrado en la ruta {Path}", context.Request.Path);

                await HandleExceptionAsync( context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (UseCaseException ex)
            {
                _logger.LogWarning(
                    ex, "Restricción del caso de uso incumplida en la ruta {Path}", context.Request.Path);

                await HandleExceptionAsync(context, HttpStatusCode.BadRequest,ex.Message);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(
                    ex, "Regla de dominio incumplida en la ruta {Path}", context.Request.Path);

                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError( ex, "Error no controlado procesando la ruta {Path}", context.Request.Path);

                await HandleExceptionAsync(context,HttpStatusCode.InternalServerError, "Ocurrió un error interno.");
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(
                new ApiResponse<object>
                {
                    Success = false,
                    Message = message
                });
        }
    }
}