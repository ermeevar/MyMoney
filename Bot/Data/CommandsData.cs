using Bot.Commands.Abstractions;

namespace Bot.Data;

/// <summary>
/// Данные по командам
/// </summary>
public static class CommandsData
{
    /// <summary>
    /// Все комманды приложения
    /// </summary>
    public static List<BaseCommand>? Commands { get; set; }
}