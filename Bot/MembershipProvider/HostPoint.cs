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
    public async Task<IEnumerable<Salary>> GetSalaryForLastYear()
    {
        var method = $"{_rootPath}/{HostUrl.Api}/{HostUrl.ApiList}/{HostUrl.SalaryList}";
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
    public async Task CreateSalary(DateTime date, double sum)
    {
        var method = $"{_rootPath}/{HostUrl.Api}/{HostUrl.DataCreator}/{HostUrl.SalaryDataCreator}";
        var data = new SalaryData { Date = date, Sum = sum };
        var response = await _client.PostAsync(new Uri(method), JsonContent.Create(data)).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.ReasonPhrase);
    }
}