using Host.Database.Configuration;
using Host.MembershipProviders;
using Host.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
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
    public IEnumerable<Salary> GetSalariesByLastYear(long chatId)
        => GetSalaries().Where(x => x.ChatId == chatId).OrderByDescending(x => x.Date).Take(12);

    /// <inheritdoc/>
    public async Task CreateSalary(DateTime date, double sum, long chatId)
    {
        var salary = GetSalaries().Where(x => x.ChatId == chatId)
            .FirstOrDefault(x => x.Date.Date == new DateTime(date.Year, date.Month, 1).Date);

        if (salary is null)
        {
            salary = new Salary
            {
                Sum = sum,
                Date = new DateTime(date.Year, date.Month, 1),
                CreatedDate = DateTime.Now,
                ChatId = chatId
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
    public async Task<double> CalcVacationPays(int days, long chatId)
    {
        var lastSalary = await _database.Salaries.Where(x => x.ChatId == chatId)
            .OrderByDescending(x => x.Date).FirstOrDefaultAsync();
        
        if (lastSalary is null) return 0;
        
        return await _calculator.CalcVacationPays(days, lastSalary.Sum);
    }

    /// <inheritdoc/>
    public async Task<double> CalcVacationDays(long chatId)
    {
        var vacationDay = await _database.VacationDays
            .Where(x => x.Id == chatId).FirstOrDefaultAsync();
        
        if (vacationDay is null)
        {
            vacationDay = new VacationDay { Id = chatId, Days = 0, UpdatedDate = DateTime.Now };
            await _database.VacationDays.AddAsync(vacationDay);
        }
        else
        {
            var uncountedDays = (DateTime.Now - vacationDay.UpdatedDate).TotalDays;

            if (uncountedDays >= 1)
            {
                var updatedDays = await _calculator.CalcVacationDays(vacationDay.Days, uncountedDays);
                vacationDay.Days = updatedDays;
                vacationDay.UpdatedDate = DateTime.Now;
                _database.VacationDays.Update(vacationDay);
            }
        }

        await _database.SaveChangesAsync();
        return vacationDay.Days;
    }
}