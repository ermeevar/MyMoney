using Host.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Urls.Host;

namespace Host.Controllers.Api;

/// <summary>
/// Контроллер списков
/// </summary>
[ApiController]
[Route($"{HostUrl.Api}/{HostUrl.ApiList}")]
public class ListController : ControllerBase
{
    /// <summary>
    /// Логирование
    /// </summary>
    private ILogger<ListController> _logger;
    
    /// <summary>
    /// Сервис с взимодействием заработной платы
    /// </summary>
    private ISalaryService _salaryService;

    /// <summary>
    /// Инициализация данных
    /// </summary>
    public ListController(ILogger<ListController> logger, ISalaryService salaryService)
    {
        _logger = logger;
        _salaryService = salaryService;
    }
    
    /// <summary>
    /// Получить список
    /// </summary>
    [HttpGet]
    [Route(HostUrl.SalaryList)]
    public IEnumerable<Salary> GetSalariesByLastYear()
    {
        try
        {
            return _salaryService.GetSalariesByLastYear();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(GetSalariesByLastYear)}: {ex.Message}");
            throw new BadHttpRequestException("Возникла ошибка при получении данных по заработной плате");
        }
    }
}