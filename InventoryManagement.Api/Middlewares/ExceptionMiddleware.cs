using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Models;
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
            catch (ConflictException ex)
            {
                _logger.LogWarning(ex, ex.Message);

                await HandleExceptionAsync(context,HttpStatusCode.Conflict, ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);

                await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context,HttpStatusCode statusCode,string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(new ApiResponse<object>
            {
                Success = false,
                Message = message
            });
        }
    }
}
