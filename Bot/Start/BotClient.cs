using Bot.Data;
using Models.Extensions;
using Models.Urls.Bot;
using Telegram.Bot;

namespace Bot.Start;

public class BotClient
{
    /// <summary>
    /// Конфигурация приложений
    /// </summary>
    private readonly IConfiguration _configuration;
    
    /// <summary>
    /// Текущий клиент
    /// </summary>
    private TelegramBotClient? _botClient;

    /// <summary>
    ///  Инициализация данных
    /// </summary>
    /// <param name="configuration"></param>
    public BotClient(IConfiguration configuration) => _configuration = configuration;

    /// <summary>
    /// Получить текущий клиент
    /// </summary>
    /// <returns>Текущий клиент</returns>
    public async Task<TelegramBotClient> GetCurrentBot()
    {
        if (_botClient is null)
        {
            _configuration.CheckKey(new[] {"Token", "Url"});
            
            _botClient = new TelegramBotClient(_configuration["Token"] ?? string.Empty);
            await _botClient.DeleteWebhookAsync();
            var webhook = $"{_configuration["Url"]}/{BotUrl.Api}/{BotUrl.ApiMessage}/{BotUrl.ApiMessageUpdate}";
            await _botClient.SetWebhookAsync(webhook);
        }
          
        return _botClient;
    }
}