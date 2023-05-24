using Utils.BackgroundServer.ConfigurationOptions;
using Utils.BackgroundServer.HostedServices;
using Utils.Infrastructure.Extensions;
using Utils.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Utils.Application.Configurations;
using Utils.CrossCuttingConcerns.Configurations;

namespace Utils.BackgroundServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureServices((hostContext, services) =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var configuration = serviceProvider.GetService<IConfiguration>();

                var appSettings = new AppSettings();

                configuration.Bind(appSettings);
                services.Configure<AppSettings>(configuration);

                services.AddDateTimeProvider();
                services.AddPersistence(appSettings.ConnectionStrings.DefaultConnection)
                        .ConfigureServiceDi();

                services.AddHostedService<ScheduleCronJobWorker>();
            });
    }
}
