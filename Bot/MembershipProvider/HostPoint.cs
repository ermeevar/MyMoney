using Models.Entities;
using Models.Http;
using Models.Urls.Host;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Bot.MembershipProvider;

/// <summary>
/// Точка доступа к хосту
/// </summary>
public class HostPoint
{
    private readonly HttpClient _client = new() { Timeout = TimeSpan.FromMinutes(2) };
    private readonly string _rootPath;

    /// <summary>
    /// Инициализация данных
    /// </summary>
    public HostPoint(IConfiguration configuration)
        => _rootPath = configuration.GetSection("Host")["Address"] ?? string.Empty;
    
    /// <summary>
    /// Получить заработную плату за последний год
    /// </summary>
    public async Task<IEnumerable<Salary>> GetSalaryForLastYear(long chatId)
    {
        var method = $"{_rootPath}/{HostUrl.Api}/{HostUrl.ApiList}/{HostUrl.SalaryList}/{chatId}";
        var response = await _client.GetAsync(new Uri(method)).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.ReasonPhrase);

        await using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<IEnumerable<Salary>>(stream).ConfigureAwait(false)
            ?? Enumerable.Empty<Salary>();
    }

    /// <summary>
    /// Создать или обновить данные по заработной плате
    /// </summary>
    /// <param name="date">Месяц заработной платы</param>
    /// <param name="sum">Сумма заработной платы</param>
    /// <param name="chatId">Идентификатор чата</param>
    public async Task CreateSalary(DateTime date, double sum, long chatId)
    {
        var method = $"{_rootPath}/{HostUrl.Api}/{HostUrl.DataCreator}/{HostUrl.SalaryDataCreator}";
        var data = new SalaryData { Date = date, Sum = sum, ChatId = chatId };
        var response = await _client.PostAsync(new Uri(method), JsonContent.Create(data)).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.ReasonPhrase);
    }

    /// <summary>
    /// Рассчитать отпускные начисления
    /// </summary>
    /// <param name="days">Количетсво дней отпуска</param>
    /// <param name="chatId">Идентификатор чата</param>
    /// <returns>Сумма начислений</returns>
    public async Task<double> CalcVacationPays(int days, long chatId)
    {
        var method = $"{_rootPath}/{HostUrl.Api}/{HostUrl.Calculator}/{HostUrl.CalcVacationPays}";
        var response = await _client.PutAsync(new Uri(method), 
            JsonContent.Create(new VacationPaysCalcData{ Days = days, ChatId = chatId })).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.ReasonPhrase);
        
        await using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<double>(stream).ConfigureAwait(false);
    }

    /// <summary>
    /// Рассчитать отпускные дни
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    /// <returns>Отпускные дни</returns>
    public async Task<double> CalcVacationDays(long chatId)
    {
        var method = $"{_rootPath}/{HostUrl.Api}/{HostUrl.Calculator}/{HostUrl.CalcVacationDays}/{chatId}";
        var response = await _client.GetAsync(new Uri(method)).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.ReasonPhrase);
        
        await using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<double>(stream).ConfigureAwait(false);
    }
}