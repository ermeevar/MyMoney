using Bot.Commands.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Extensions;

/// <summary>
/// Дополнение к <see cref="BaseCommand"/>
/// </summary>
public static class BaseCommandExtension
{
    /// <summary>
    /// Получить кнопку из комманды
    /// </summary>
    /// <param name="command">Комманда</param>
    /// <returns>Кнопка</returns>
    public static KeyboardButton ToButton(this BaseCommand command)
        => new (command.Name);
}