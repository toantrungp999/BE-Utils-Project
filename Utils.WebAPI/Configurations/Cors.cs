using Utils.CrossCuttingConcerns.Constants;

namespace Utils.WebAPI.Configurations
{
    public static class Cors
    {
        public static void AllowCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(ConfigurationConstant.CorsPolicy,
                    builder =>
                    {
                        builder
                            //.WithOrigins(configuration.GetSection(ConfigurationConstant.AllowedOrigins).Get<string[]>())
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }
    }
}
