namespace Models.Http;

/// <summary>
/// Данные рассчета отпускных начислений
/// </summary>
public class VacationPaysCalcData
{
    /// <summary>
    /// Количество дней отпуска
    /// </summary>
    public int Days { get; set; }
    
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public long ChatId { get; set; }
}