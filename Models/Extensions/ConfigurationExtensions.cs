using Microsoft.Extensions.Configuration;
using Models.Exceptions;

namespace Models.Extensions;

/// <summary>
/// Дополнение к <see cref="IConfiguration"/>
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Проверить ключ на валидность
    /// </summary>
    /// <param name="configuration">Абстракция конфигурации приложения</param>
    /// <param name="key">Наименование ключа</param>
    /// <exception cref="Exception">Ошибка конфигурации</exception>
    public static void CheckKey(this IConfiguration configuration, string key)
    {
        if (configuration[key] is null)
            throw ConfigurationException.CreateInvalidKeyException(key);
    }
    
    /// <summary>
    /// Проверить список ключей на валидность
    /// </summary>
    /// <param name="configuration">Абстракция конфигурации приложения</param>
    /// <param name="keys">Список наименований ключей</param>
    /// <exception cref="Exception">Ошибка конфигурации</exception>
    public static void CheckKey(this IConfiguration configuration, IEnumerable<string> keys)
    {
        foreach (var key in keys)
            configuration.CheckKey(key); 
    }   
}