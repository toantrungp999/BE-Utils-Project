using Utils.Application.Services;
using Utils.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Utils.Application
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, Action<Type, Type, ServiceLifetime> configureInterceptor = null)
        {
            services.AddScoped<ITestService, TestService>();
            //services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
