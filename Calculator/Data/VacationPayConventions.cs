namespace Calculator.Data;

/// <summary>
/// Статичные данные для расчета отпускных
/// </summary>
public static class VacationPayConventions
{
    /// <summary>
    /// Среднее количество календарных дней в месяце
    /// </summary>
    public const double AverageDaysOfMonth = 29.3;

    /// <summary>
    /// Коэффициент накопления отпускных дней на 1 день
    /// </summary>
    public const double AverageVacationDayCoef = 0.08;
}