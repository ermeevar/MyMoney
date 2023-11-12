using System;

namespace Calculator.Entities.Http;

/// <summary>
/// Информация по запросу
/// </summary>
public class Result
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Дополнительная информация
    /// </summary>
    public object Data { get; set; }
    
    /// <summary>
    /// Успешно ли прошел запрос
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Создать ответ
    /// </summary>
    /// <param name="data">Информация</param>
    /// <param name="isSuccess">Успешно ли проше запрос</param>
    /// <param name="message">Сообщение</param>
    /// <returns>Упаковка ответа на запрос</returns>
    public static Result Create(object data, bool isSuccess = true, string message = "")
        => new Result {Data = data, IsSuccess = isSuccess, Message = message};
}