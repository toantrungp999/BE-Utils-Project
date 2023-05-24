using Microsoft.IdentityModel.Tokens;
using System.Text;
using Utils.CrossCuttingConcerns.Constants;

namespace Utils.WebAPI.Configurations
{
    public static class Authentication
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection(ConfigurationConstant.SecretKey).Value!;

            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Secret").Value!))
                };
            });
        }
    }
}
