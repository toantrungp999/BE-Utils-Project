using Utils.CrossCuttingConcerns.OS;
using Utils.Infrastructure.HostedServices;
using Utils.Infrastructure.HostedServices.Interfaces;
using Utils.Infrastructure.OS;
using Microsoft.Extensions.DependencyInjection;

namespace Utils.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, string migrationAssembly = "")
        {
            return services;
        }

        public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }

        public static IServiceCollection AddBackgroundService(this IServiceCollection services)
        {
            services.AddSingleton(typeof(QueueService<>));
            services.AddHostedService<MyQueueService1>();
            services.AddHostedService<MyQueueService2>();

            return services;
        }
    }
}
