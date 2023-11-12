using Models.Entities;

namespace Host.Services.Abstractions;

/// <summary>
/// Предоставялет возможность взаимодействия с заработной платой
/// </summary>
public interface ISalaryService
{
    /// <summary>
    /// Получить заработную плату за все время
    /// </summary>
    IEnumerable<Salary> GetSalaries();

    /// <summary>
    /// Получить заработную плату за последний год
    /// </summary>
    IEnumerable<Salary> GetSalariesByLastYear();

    /// <summary>
    /// Добавить заработную плату или заменить на новую <paramref name="sum"/>
    /// </summary>
    /// <param name="date">Дата заработной платы</param>
    /// <param name="sum">Сумма</param>
    Task CreateSalary(DateTime date, double sum);
}