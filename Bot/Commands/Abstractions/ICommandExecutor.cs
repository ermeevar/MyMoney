using Telegram.Bot.Types;

namespace Bot.Commands.Abstractions;

/// <summary>
/// Исполняющий команду
/// </summary>
public interface ICommandExecutor
{
    /// <summary>
    /// Исполнить комманду
    /// </summary>
    /// <param name="update">Информация отправленная с бота</param>
    Task ExecuteAsync(Update update);
}