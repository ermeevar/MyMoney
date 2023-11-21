using Models.Http;
using Models.Urls.Calculator;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Host.MembershipProviders;

/// <summary>
/// Точка доступа к калькулятору
/// </summary>
public class CalculatorPoint
{
    private readonly HttpClient _client = new() { Timeout = TimeSpan.FromMinutes(2) };
    private readonly string _rootPath;

    /// <summary>
    /// Инициализация данных
    /// </summary>
    public CalculatorPoint(IConfiguration configuration)
        => _rootPath = configuration.GetSection("Calculator")["Address"] ?? string.Empty;

    /// <summary>
    /// Рассчитать отпускные начисления
    /// </summary>
    /// <param name="days">Количество дней в отпуске</param>
    /// <param name="salary">Заработная плата</param>
    /// <returns>Сумма отпускных начислений</returns>
    public async Task<double> CalcVacationPays(int days, double salary)
    {
        var method = $"{_rootPath}/{CalculatorUrl.VacationPays}/{CalculatorUrl.VacationPayByCountDayRound}";
        var data = new VacationPaysData { Wage = salary, Days = days, Round = 2 };
        var response = await _client.PostAsync(new Uri(method), JsonContent.Create(data)).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.ReasonPhrase);

        await using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<double>(stream).ConfigureAwait(false);
    }
}