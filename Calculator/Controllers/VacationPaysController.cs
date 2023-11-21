using System.ComponentModel.DataAnnotations;
using Models.Http;
using Calculator.Services.Abstractions;
using Calculator.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using static Calculator.Start.CastleWindsorConfiguration.Container;
using CalculatorUrl = Models.Urls.Calculator.CalculatorUrl;

namespace Calculator.Controllers;

/// <summary>
/// Контроллер расчета отпускных начислений
/// </summary>
[ApiController]
[Route(CalculatorUrl.VacationPays)]
public class VacationPaysController
{
    private readonly ILogger<VacationPaysService> _logger;
    private IVacationPaysService VacationPaysService { get; }

    public VacationPaysController(ILogger<VacationPaysService> logger)
    {
        VacationPaysService = CurrentContainer.Resolve<IVacationPaysService>();
        _logger = logger;
    }
    
    /// <summary>
    /// Расчитать примерные отпускные начисления по количеству дней отпуска
    /// </summary>
    /// <param name="wage">Заработная плата</param>
    /// <param name="countDaysOfVacation">Количество дней в отпуске</param>
    /// <returns>Отпускные начисления</returns>
    [HttpPost]
    [Route(CalculatorUrl.VacationPayByCountDay)]
    public double GetDefaultVacationPays([Required] double wage, [Required] int countDaysOfVacation)
    {
        try
        {
            return VacationPaysService.CalculateDefaultPaysByVacationDays(wage, countDaysOfVacation);
        }
        catch (ValidationException ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(GetDefaultVacationPays)}: {ex.Message}");
            throw new BadHttpRequestException(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(GetDefaultVacationPays)}: {ex.Message}");
            throw new BadHttpRequestException("Возникла ошибка при расчете отпускных начислений");
        }
    }

    /// <summary>
    /// Расчитать примерные отпускные начисления по количеству дней отпуска и округлить
    /// </summary>
    /// <param name="data">Данные</param>
    /// <returns>Отпускные начисления</returns>
    [HttpPost]
    [Route(CalculatorUrl.VacationPayByCountDayRound)]
    public double GetRoundedDefaultVacationPays([Required] VacationPaysData data)
    {
        try
        {
            return VacationPaysService.GetRoundedDefaultVacationPays(data.Wage, data.Days, data.Round);
        }
        catch (ValidationException ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(GetRoundedDefaultVacationPays)}: {ex.Message}");
            throw new BadHttpRequestException(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(GetRoundedDefaultVacationPays)}: {ex.Message}");
            throw new BadHttpRequestException("Возникла ошибка при расчете отпускных начислений");
        }
    }
}