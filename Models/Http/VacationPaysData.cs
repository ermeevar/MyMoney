namespace Models.Http;

/// <summary>
/// Объект для рассчета данных
/// </summary>
public class VacationPaysData
{
    /// <summary>
    /// Заработная плата
    /// </summary>
    public double Wage { get; set; }
    
    /// <summary>
    /// Количество дней в отпуске
    /// </summary>
    public int Days { get; set; }
    
    /// <summary>
    /// Количество цифр после запятой, с которыми надо округлять число
    /// </summary>
    public int Round { get; set; }
}