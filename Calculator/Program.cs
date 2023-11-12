using Calculator.Start;
using NLog.Web;

namespace Calculator;

internal static class Program
{
    /// <summary>
    /// Точка входа
    /// </summary>
    private static void Main(string[] args)
     => CreateHostBuilder(args).Build().Run();

    /// <summary>
    /// Создать и настроить приложение
    /// </summary>
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .UseSystemd()
            .UseNLog()
            // Конфигурация хоста
            .ConfigureServices(services =>
            {
                services.AddHostedService<LifetimeEventsHostedService>();
                services.Configure<HostOptions>(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30));
            })
            // Конфигурация веба
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<StartUpConfiguration>();
            });
}