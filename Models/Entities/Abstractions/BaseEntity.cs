using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Entities.Abstractions;

/// <summary>
/// Абстракная сущность 
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор сущности
    /// </summary>
    [Column("id")]
    [JsonPropertyName("id")]
    public virtual long Id { get; set; }
}