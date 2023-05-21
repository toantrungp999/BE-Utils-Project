using Utils.Application;
using Utils.BackgroundServer.ConfigurationOptions;
using Utils.BackgroundServer.HostedServices;
using Utils.Infrastructure.Extensions;
using Utils.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

                var appSettings = new AppSetting();
                configuration.Bind(appSettings);
                services.Configure<AppSetting>(configuration);

                services.AddDateTimeProvider();
                services.AddPersistence(appSettings.ConnectionStrings.DefaultConnection)
                        .AddApplicationServices();

                services.AddHostedService<ScheduleCronJobWorker>();
            });
    }
}
