using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PropertyService.Application.Dtos;
using PropertyService.Domain.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PropertyService.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlerMiddleware> logger)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var response = new ErrorResponseDto();

            switch (exception)
            {
                case PropertyDomainException domainException:
                    statusCode = HttpStatusCode.BadRequest;
                    response.Message = domainException.Message;
                    response.Details = $"Domain Error Code: {domainException.Code}";
                    break;
                default:
                    response.Message = "An unexpected error occurred.";
                    response.Details = exception.Message; // In production, you might not want to expose this.
                    break;
            }

            logger.LogError(exception, "An error occurred while processing the request.");

            response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
