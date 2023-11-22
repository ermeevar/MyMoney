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
    IEnumerable<Salary> GetSalariesByLastYear(long chatId);

    /// <summary>
    /// Добавить заработную плату или заменить на новую <paramref name="sum"/>
    /// </summary>
    /// <param name="date">Дата заработной платы</param>
    /// <param name="sum">Сумма</param>
    /// <param name="chatId">Идентификатор чата</param>
    Task CreateSalary(DateTime date, double sum, long chatId);

    /// <summary>
    /// Рассчитать отпусные начисления
    /// </summary>
    /// <param name="days">Количество дней отпуска</param>
    /// <param name="chatId">Идентификатор чата</param>
    /// <returns>Сумма отпускных начислений</returns>
    Task<double> CalcVacationPays(int days, long chatId);

    /// <summary>
    /// Рассчитать отпусные начисления
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    Task<double> CalcVacationDays(long chatId);
}