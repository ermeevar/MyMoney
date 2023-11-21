using Bot.Commands.Abstractions;

namespace Bot.Data;

/// <summary>
/// Данные по командам
/// </summary>
public static class CommandsData
{
    /// <summary>
    /// Операции пользователя для колбэка [идентификатор чата, операция]
    /// </summary>
    public static IDictionary<long, string> UserOperations { get; } = new Dictionary<long, string>();

    /// <summary>
    /// Все комманды приложения
    /// </summary>
    public static List<BaseCommand>? Commands { get; set; }
}