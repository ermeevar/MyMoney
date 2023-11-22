using System.ComponentModel.DataAnnotations;
using Calculator.Data;
using Calculator.Services.Abstractions;

namespace Calculator.Services.Implementations;

/// <summary>
/// Сервис расчета отпускных начислений
/// </summary>
public class VacationPaysService : IVacationPaysService
{
    /// <inheritdoc/>
    public double CalculateDefaultPaysByVacationDays(double wage, int countDaysOfVacation)
    {
        if (wage <= 0 || countDaysOfVacation <= 0)
            throw new ValidationException("Некорректное значение заработной платы или количества дней отпуска! " +
                                          $"Заработная плата: {wage}, Количество дней: {countDaysOfVacation}");
        
        return wage / VacationPayConventions.AverageDaysOfMonth * countDaysOfVacation;
    }

    /// <inheritdoc/>
    public double GetRoundedDefaultVacationPays(double wage, int countDaysOfVacation, int digital)
    {
        if(digital < 0)
            throw new ValidationException("Количество цифр после запятой не может быть отрицательным! " +
                                          $"Количество знаков после запятой: {digital}");
        
        return Math.Round(CalculateDefaultPaysByVacationDays(wage, countDaysOfVacation), digital);
    }

    /// <inheritdoc/>
    public double CalcVacationDaysPays(double countedDays, double uncountedDays)
    {
        if (countedDays < 1 || uncountedDays < 1)
            throw new Exception($"Некорректно получены данные {countedDays}/{uncountedDays}");
        
        return Math.Round(countedDays + uncountedDays * VacationPayConventions.AverageVacationDayCoef, 2);
    }
}