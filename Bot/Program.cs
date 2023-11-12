using Bot.Start;

namespace Bot;

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