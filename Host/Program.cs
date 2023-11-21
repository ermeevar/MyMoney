using Host.Start;
using NLog.Web;

namespace Host;

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
        Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .UseSystemd()
            .UseNLog()
            // Конфигурация хоста
            .ConfigureServices(services =>
            {
                services.Configure<HostOptions>(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30));
            })
            // Конфигурация веба
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<StartUpConfiguration>();
            });
}