using System.ComponentModel.DataAnnotations;
using Calculator.Entities.Http;
using Calculator.Services.Abstractions;
using Calculator.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using static Calculator.Start.CastleWindsorConfiguration.Container;

namespace Calculator.Controllers;

/// <summary>
/// Контроллер расчета отпускных начислений
/// </summary>
[ApiController]
[Route("vacationpays")]
public class VacationPaysController
{
    private ILogger<VacationPaysService> _logger;
    public IVacationPaysService VacationPaysService { get; }

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
    [HttpGet]
    [Route("default")]
    public Result GetDefaultVacationPays([Required] double wage, [Required] int countDaysOfVacation)
    {
        try
        {
            return Result.Create(VacationPaysService.CalculateDefaultPaysByVacationDays(wage, countDaysOfVacation));
        }
        catch (ValidationException ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(GetDefaultVacationPays)}: {ex.Message}");
            return Result.Create(null, false, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(GetDefaultVacationPays)}: {ex.Message}");
            return Result.Create(null, false, "Возникла ошибка при расчете отпускных начислений");
        }
    }

    /// <summary>
    /// Расчитать примерные отпускные начисления по количеству дней отпуска и округлить
    /// </summary>
    /// <param name="wage">Заработная плата</param>
    /// <param name="countDaysOfVacation">Количество дней в отпуске</param>
    /// <param name="digital">Количество цифр после запятой, с которыми надо округлять число</param>
    /// <returns>Отпускные начисления</returns>
    [HttpGet]
    [Route("default/round")]
    public Result GetRoundedDefaultVacationPays([Required] double wage, [Required] int countDaysOfVacation, [Required] int digital)
    {
        try
        {
            return Result.Create(VacationPaysService.GetRoundedDefaultVacationPays(wage, countDaysOfVacation, digital));
        }
        catch (ValidationException ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(GetRoundedDefaultVacationPays)}: {ex.Message}");
            return Result.Create(null, false, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Возникла ошибка при вызове метода {nameof(GetRoundedDefaultVacationPays)}: {ex.Message}");
            return Result.Create(null!, false, "Возникла ошибка при расчете отпускных начислений");
        }
    }
}