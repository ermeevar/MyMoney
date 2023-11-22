using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Models.Entities.Abstractions;

namespace Models.Entities;

/// <summary>
/// Накопленные отпускные дни
/// </summary>
[Table("vacation_free_days")]
public class VacationDay : BaseEntity
{
    /// <summary>
    /// Количество дней
    /// </summary>
    [Column("days")]
    [JsonPropertyName("days")]
    public virtual double Days { get; set; }
    
    /// <summary>
    /// Последний раз обновленная информация
    /// </summary>
    [Column("updated_date")]
    [JsonPropertyName("updatedDate")]
    public virtual DateTime UpdatedDate { get; set; }
}