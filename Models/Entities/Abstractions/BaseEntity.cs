using System.ComponentModel.DataAnnotations.Schema;

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
    public virtual long Id { get; set; }
}