using Bot.Commands.Abstractions;
using Bot.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Bot.Controllers;

/// <summary>
/// Контроллер обработки сообщений
/// </summary>
[ApiController, Route(BotClientConventions.ApiMessageRoute)]
public class BotController : ControllerBase
{
    /// <summary>
    /// Исполнитель комманд
    /// </summary>
    private readonly ICommandExecutor _executor;
    
    /// <summary>
    /// Проинициализировать данные
    /// </summary>
    public BotController(ICommandExecutor executor) => _executor = executor;
    
    /// <summary>
    /// Обработать сообщение
    /// </summary>
    /// <param name="update">Оболочка информации отправляемая с Telegram бота</param>
    [HttpPost, Route(BotClientConventions.ApiMessageUpdatePostfix)]
    public async Task<IActionResult> Update([FromBody]object update)
    {
        var deserializedData = JsonConvert.DeserializeObject<Update>(update.ToString() ?? string.Empty);

        if (deserializedData?.Message?.Chat is null && deserializedData?.CallbackQuery == null)
            return Ok();
        
        try
        {
            await _executor.ExecuteAsync(deserializedData);
        }
        catch (Exception)
        {
            return Ok();
        }
            
        return Ok();
    }
}