using Utils.WebAPI.Configurations.Middlewares;

namespace Utils.WebAPI.Configurations
{
    public static class PresentationExtension
    {
        public static IApplicationBuilder UseCustomResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomResponseWrapper>();
        }

        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
