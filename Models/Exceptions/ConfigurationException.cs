namespace Models.Exceptions;

/// <summary>
/// Ошибка конфигурации
/// </summary>
public abstract class ConfigurationException : Exception
{
    /// <summary>
    /// Создать исключение типа <see cref="Exception"/>
    /// </summary>
    /// <param name="message">Сообщение ошибки конфигурации</param>
    /// <returns>Исключение</returns>
    public static Exception Create(string message)
        => new Exception($"Ошибка конфигурации приложения: {message}");
    
    /// <summary>
    /// Создать исключение невалидности ключа конфигурации
    /// </summary>
    /// <param name="key">Наименование ключа</param>
    /// <returns>Исключение</returns>
    public static Exception CreateInvalidKeyException(string key)
        => new Exception($"Ошибка конфигурации приложения: некорректно указан ключ конфигурации [{key}]");
}