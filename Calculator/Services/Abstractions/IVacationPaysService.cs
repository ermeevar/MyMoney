namespace Calculator.Services.Abstractions;

/// <summary>
/// Предоставляет возможность расчета отпускных начислений
/// </summary>
public interface IVacationPaysService
{
    /// <summary>
    /// Рассчитать примерный размер отпускных начислений 
    /// </summary>
    /// <param name="wage">Размер заработной платы</param>
    /// <param name="countDaysOfVacation">Количество дней в выбранном отпуске</param>
    /// <returns>Отпускные начисления</returns>
    double CalculateDefaultPaysByVacationDays(double wage, int countDaysOfVacation);
    
    /// <summary>
    /// Рассчитать примерный размер отпускных начислений и округлить
    /// </summary>
    /// <param name="wage">Размер заработной платы</param>
    /// <param name="countDaysOfVacation">Количество дней в выбранном отпуске</param>
    /// <param name="digital">Количество цифр после запятой, с которыми надо округлять число</param>
    /// <returns>Отпускные начисления</returns>
    double GetRoundedDefaultVacationPays(double wage, int countDaysOfVacation, int digital);

    /// <summary>
    /// Рассчитать отпускные дни
    /// </summary>
    /// <param name="countedDays">Hакопленные дни</param>
    /// <param name="uncountedDays">Нерассчитанные дни</param>
    /// <returns>Отпускные дни</returns>
    double CalcVacationDaysPays(double countedDays, double uncountedDays);
}