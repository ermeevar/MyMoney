using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Host.Database.Configuration;

/// <summary>
/// Конфигурация  базы данных
/// </summary>
public class DataContext : DbContext
{
    /// <inheritdoc/>
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    // TODO перенести в маппинги
    public DbSet<Salary> Salaries { get; set; }
}