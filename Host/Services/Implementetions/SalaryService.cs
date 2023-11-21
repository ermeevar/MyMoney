using Host.Database.Configuration;
using Host.MembershipProviders;
using Host.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Models.Entities;

namespace Host.Services.Implementetions;

/// <inheritdoc/>
public class SalaryService : ISalaryService
{
    /// <summary>
    /// БД
    /// </summary>
    private readonly DataContext _database;

    private readonly CalculatorPoint _calculator;

    /// <summary>
    /// Инициализация данных
    /// </summary>
    public SalaryService(DataContext database, CalculatorPoint calculator)
    {
        _database = database;
        _calculator = calculator;
    }
    
    /// <inheritdoc/>
    public IEnumerable<Salary> GetSalaries()
        => _database.Salaries;

    /// <inheritdoc/>
    public IEnumerable<Salary> GetSalariesByLastYear()
    {
        var nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var yearAgoDate = new DateTime(nowDate.AddMonths(-12).Year, nowDate.AddMonths(-12).Month, 1);
        return GetSalaries().Where(x => x.Date.Date >= yearAgoDate.Date && x.Date.Date <= nowDate.Date);
    }

    /// <inheritdoc/>
    public async Task CreateSalary(DateTime date, double sum)
    {
        var salary = GetSalaries()
            .FirstOrDefault(x => x.Date.Date == new DateTime(date.Year, date.Month, 1).Date);

        if (salary is null)
        {
            salary = new Salary
            {
                Sum = sum,
                Date = new DateTime(date.Year, date.Month, 1),
                CreatedDate = DateTime.Now
            };
            await _database.AddAsync(salary);
        }
        else
        {
            salary.Sum = sum;
            _database.Update(salary);
        }

        await _database.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<double> CalcVacationPays(int days)
    {
        var lastSalary = await _database.Salaries.OrderByDescending(x => x.Date).FirstAsync();
        return await _calculator.CalcVacationPays(days, lastSalary.Sum);
    }
}