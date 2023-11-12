using Bot.Commands.Implementations;
using Bot.Commands.Implementations.Lists;
using Bot.Data;
using Bot.Extensions;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands.Abstractions;

/// <summary>
/// Команда
/// </summary>
public abstract class BaseCommand
{
    /// <summary>
    /// Текущий клиент
    /// </summary>
    protected TelegramBotClient CurrentClient { get; }
    
    /// <summary>
    /// Ключ комманды
    /// </summary>
    public abstract string Key { get; }
    
    /// <summary>
    /// Наименование комманды
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Инициализация данных
    /// </summary>
    protected BaseCommand(BotClient telegramBot) 
        => CurrentClient = telegramBot.GetCurrentBot().Result;
    
    /// <inheritdoc cref="ICommandExecutor.ExecuteAsync"/>
    public abstract Task ExecuteAsync(Update update);
    
    /// <summary>
    /// Получить команду из списка по ключу
    /// </summary>
    /// <typeparam name="T">Тип команды</typeparam>
    /// <returns>Команда</returns>
    internal static BaseCommand GetCommand<T>() where T: BaseCommand
    {
        var commands = CommandsData.Commands;
        var currentCommand = commands!.FirstOrDefault(x => x.GetType() == typeof(T)) 
                             ?? commands!.First(x => x.GetType() == typeof(UnknownCommand));
    
        return currentCommand;
    }
    
    /// <summary>
    /// Создать кнопки
    /// </summary>
    /// <returns>Абстракция интерфейа взаимодействия</returns>
    internal virtual IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>());
}