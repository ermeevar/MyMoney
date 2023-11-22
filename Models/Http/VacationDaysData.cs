namespace Models.Http;

/// <summary>
/// Расчет отпусных дней
/// </summary>
public class VacationDaysData
{
    /// <summary>
    /// Уже рассчитанные(накопленные) дни
    /// </summary>
    public double VacationDays { get; set; }
    
    /// <summary>
    /// Нерассчитанные дни
    /// </summary>
    public double UncountedDays { get; set; }
}