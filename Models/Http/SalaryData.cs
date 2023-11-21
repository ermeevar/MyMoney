namespace Models.Http;

/// <summary>
/// Данные заработной платы
/// </summary>
public class SalaryData
{
    /// <summary>
    /// За какой месяц 
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Сумма
    /// </summary>
    public double Sum { get; set; }
}