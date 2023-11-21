using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Models.Entities.Abstractions;

namespace Models.Entities;

/// <summary>
/// Сущщность заработной платы
/// </summary>
[Table("salaries")]
public class Salary : BaseEntity
{
    /// <summary>
    /// Сумма заработной платы
    /// </summary>
    [Column("sum")]
    [JsonPropertyName("sum")]
    public virtual double Sum { get; set; }
    
    /// <summary>
    /// Дата заработной платы 
    /// </summary>
    [Column("date")]
    [JsonPropertyName("date")]
    public virtual DateTime Date { get; set; }
    
    /// <summary>
    /// Путь до файла расчетного листа
    /// </summary>
    [Column("vacation_pay_path")]
    [JsonPropertyName("VacationPayPath")]
    public virtual string? VacationPayPath { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    [Column("date_create")]
    [JsonPropertyName("CreatedDate")]
    public virtual DateTime? CreatedDate { get; set; }
}