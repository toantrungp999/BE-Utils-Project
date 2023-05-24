using Utils.Application.Services;
using Utils.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Utils.Application.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServiceDi(this IServiceCollection services, Action<Type, Type, ServiceLifetime> configureInterceptor = null)
        {
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
