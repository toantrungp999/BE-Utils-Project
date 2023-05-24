using Microsoft.AspNetCore.Authorization;
using System.Configuration;
using System.Reflection;
using Utils.Application.Configurations;
using Utils.Infrastructure.Extensions;
using Utils.Persistence.Contexts;
using Utils.Persistence.Extensions;

namespace Utils.WebAPI.Configurations
{
    public static class DependencyInjection
    {
        /// <summary>
     /// Configure the dependency injection for entire solution.
     /// </summary>
        public static void ConfigureDi(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.ConfigureServiceDi()
                .AddPersistence(connectionString, typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name)
                .AddDateTimeProvider()
                .AddBackgroundService()
                .ConfigureOthers(configuration);
        }

        private static void ConfigureOthers(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAuthentication(configuration);
        }
    }
}
