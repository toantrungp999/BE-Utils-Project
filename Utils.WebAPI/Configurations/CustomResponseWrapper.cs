using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Utils.CrossCuttingConcerns.Constants;
using Utils.CrossCuttingConcerns.Extensions;
using Utils.Persistence.Extensions;
using Utils.WebAPI.Responses;

namespace Utils.WebAPI.Configurations
{
    /// <summary>
    /// Middleware for wrapping the response in a meaningful format.
    /// </summary>
	public class CustomResponseWrapper
    {
        private readonly RequestDelegate _next;

        public CustomResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.HasSwaggerRequest())
            {
                await _next(httpContext);
                return;
            }

            var originalResponseBody = httpContext.Response.Body;

            // Because we will wrap the response into a meaningful object,
            // therefore the Response's ContentType must be set before processing further.
            httpContext.Response.OnStarting(() =>
            {
                httpContext.Response.ContentType = ConfigurationConstant.DefaultContentType;
                return Task.CompletedTask;
            });

            using (var tempStream = new MemoryStream())
            {
                httpContext.Response.Body = tempStream;

                await _next(httpContext);

                if (httpContext.Response.StatusCode != (int)HttpStatusCode.NoContent)
                {
                    await BuildResponseWrapper(httpContext, originalResponseBody);
                }

                httpContext.Response.Body = originalResponseBody;
            }
        }

        private async Task BuildResponseWrapper(HttpContext httpContext, Stream originalResponseBody)
        {
            var responseBody = httpContext.Response.Body;
            responseBody.Position = 0;

            var apiVersion = httpContext.Features.Get<IApiVersioningFeature>()?.RequestedApiVersion;
            var bodyStr = await new StreamReader(responseBody).ReadToEndAsync();
            var response = TransformResult(bodyStr, apiVersion, httpContext.Response.StatusCode);

            var buffer = Encoding.UTF8.GetBytes(response);

            using (var output = new MemoryStream(buffer))
            {
                httpContext.Response.ContentLength = buffer.Length;
                await output.CopyToAsync(originalResponseBody);
            }
        }

        private string TransformResult(string result, ApiVersion apiVersion, int statusCode)
        {
            const int startedHttpErrorStatusCode = 400;

            var baseResponse = new BaseResponse
            {
                IsSuccess = false,
                ApiVersion = apiVersion != null
                    ? $"{apiVersion.MajorVersion}.{apiVersion.MinorVersion ?? 0}"
                    : ConfigurationConstant.DefaultApiVersion,
                StatusCode = statusCode
            };

            switch (statusCode)
            {
                case var _ when statusCode < startedHttpErrorStatusCode:
                    if (result.TryParse(out ErrorResponseWrapper errorWrapper1) &&
                        errorWrapper1 != null &&
                        !errorWrapper1.Errors.IsNullOrEmpty())
                    {
                        baseResponse.Data = errorWrapper1.Errors;

                        break;
                    }

                    baseResponse.IsSuccess = true;
                    baseResponse.Data = result.ToObject<object>();

                    break;

                case var _ when statusCode == (int)HttpStatusCode.Forbidden:
                    baseResponse.Data = new List<ErrorResponse>
                    {
                        new ErrorResponse
                        {
                            Message = ExceptionConstant.RestrictedResource
                        }
                    };

                    break;

                default:
                    if (result.TryParse(out ErrorResponseWrapper errorWrapper) &&
                        errorWrapper != null &&
                        !errorWrapper.Errors.IsNullOrEmpty())
                    {
                        baseResponse.Data = errorWrapper.Errors;

                        break;
                    }

                    baseResponse.Data = result.ToObject<object>();

                    break;
            }

            return JsonConvert.SerializeObject(baseResponse);
        }
    }
}
