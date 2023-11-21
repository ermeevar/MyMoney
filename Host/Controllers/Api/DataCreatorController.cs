using System.ComponentModel.DataAnnotations;
using Host.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Urls.Host;

namespace Host.Controllers.Api;

/// <summary>
/// Контроллер создания данных
/// </summary>
[ApiController]
[Route($"{HostUrl.Api}/{HostUrl.DataCreator}")]
public class DataCreatorController : ControllerBase
{
    /// <summary>
    /// Логирование
    /// </summary>
    private readonly ILogger<ListController> _logger;
    
    /// <summary>
    /// Сервис с взимодействием заработной платы
    /// </summary>
    private ISalaryService _salaryService;

    /// <summary>
    /// Инициализация данных
    /// </summary>
    public DataCreatorController(ILogger<ListController> logger, ISalaryService salaryService)
    {
        _logger = logger;
        _salaryService = salaryService;
    }
    
    /// <summary>
    /// Добавить заработную плату
    /// </summary>
    [HttpPost]
    [Route(HostUrl.SalaryDataCreator)]
    public async Task CreateSalary([Required] DateTime date, [Required] double sum)
    {
        try
        {
            await _salaryService.CreateSalary(date, sum);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(CreateSalary)}: {ex.Message}");
            throw new BadHttpRequestException("Возникла ошибка при попытке добавлении заработной платы");
        }
    }
}