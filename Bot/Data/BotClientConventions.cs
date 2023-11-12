namespace Bot.Data;

/// <summary>
/// Статичные данные
/// </summary>
public static class BotClientConventions
{
    /// <summary>
    /// Разделитель для запросов
    /// </summary>
    public const string UrlDelimiter = "/";
    
    /// <summary>
    /// Мап контроллера сообщений
    /// </summary>
    public const string ApiMessageRoute = "api/message";
    
    /// <summary>
    /// Дополнение к методу обновлению
    /// </summary>
    public const string ApiMessageUpdatePostfix = "update";
}