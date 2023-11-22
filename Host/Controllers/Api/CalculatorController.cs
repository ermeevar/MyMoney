using System.ComponentModel.DataAnnotations;
using Host.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Http;
using Models.Urls.Host;

namespace Host.Controllers.Api;

/// <summary>
/// Контроллер рассчета данных
/// </summary>
[ApiController]
[Route($"{HostUrl.Api}/{HostUrl.Calculator}")]
public class CalculatorController
{
    /// <summary>
    /// Логирование
    /// </summary>
    private readonly ILogger<CalculatorController> _logger;
    
    /// <summary>
    /// Сервис с взимодействием заработной платы
    /// </summary>
    private ISalaryService _salaryService;

    /// <summary>
    /// Инициализация данных
    /// </summary>
    public CalculatorController(ILogger<CalculatorController> logger, ISalaryService salaryService)
    {
        _logger = logger;
        _salaryService = salaryService;
    }
    
    /// <summary>
    /// Рассчитать отпускные начисления
    /// </summary>
    [HttpPut]
    [Route(HostUrl.CalcVacationPays)]
    public async Task<double> CalcVacationPays([Required] VacationPaysCalcData data)
    {
        try
        {
            return await _salaryService.CalcVacationPays(data.Days, data.ChatId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(CalcVacationPays)}: {ex.Message}");
            throw new BadHttpRequestException("Возникла ошибка при попытке расчета отпускных начислений");
        }
    }
    
    /// <summary>
    /// Рассчитать отпускные дни
    /// </summary>
    [HttpGet]
    [Route(HostUrl.CalcVacationDays + "/{chatId:long}")]
    public async Task<double> CalcVacationDays([FromRoute] long chatId)
    {
        try
        {
            return await _salaryService.CalcVacationDays(chatId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(CalcVacationDays)}: {ex.Message}");
            throw new BadHttpRequestException("Возникла ошибка при попытке расчета отпускных дней");
        }
    }
}