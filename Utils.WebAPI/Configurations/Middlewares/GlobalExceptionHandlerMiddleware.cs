using Utils.CrossCuttingConcerns.Exceptions;
using Utils.Infrastructure.Constants;
using System.Net;
using Newtonsoft.Json;
using System;
using Utils.WebAPI.Responses;

namespace Utils.WebAPI.Configurations.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                response.ContentType = ConfigurationConstant.DefaultContentType;

                switch (ex)
                {
                    case ValidationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        _logger.LogError(ex, $"{DateTimeOffset.Now.Ticks}-{Environment.CurrentManagedThreadId}-{ex.Message}");
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                ErrorResponseWrapper errorResponseWrapper = new ErrorResponseWrapper
                {
                    Errors = new List<ErrorResponse>
                    {
                        new ErrorResponse
                        {
                        Message = ex.Message,
                        MessageDetail = ex.InnerException?.Message
                    }
                }
                };

                await response.WriteAsync(JsonConvert.SerializeObject(errorResponseWrapper));
            }

        }
    }
}
