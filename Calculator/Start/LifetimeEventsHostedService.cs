using NLog.Web;
using NLog;

namespace Calculator.Start;

/// <summary>
/// Сервис настройки жизнеспособности хоста
/// </summary>
internal class LifetimeEventsHostedService : IHostedService
{
    private Logger Logger => LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    
    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Logger.Info("Начало загрузки модуля");
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        Logger.Info("Деинсталяция модуля");
        return Task.CompletedTask;
    }
}