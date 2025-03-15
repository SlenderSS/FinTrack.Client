using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace FinTrack.Client.Services.Implementation;

public class BudgetService : IBudgetService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:44352/api/Budget"; 
    public BudgetService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_baseUrl);
    }

    public async Task<Result> CreateBudget(int userId, int currencyId, Budget budget)
    {
        try
        {
            HttpRequestMessage message =
            new HttpRequestMessage(
                HttpMethod.Post,
                _httpClient.BaseAddress + $"?userId={userId}&currencyId={currencyId}");
            var content = JsonConvert.SerializeObject(budget);

            message.Content = new StringContent(content, Encoding.UTF8, "application/json");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using var response = await _httpClient.SendAsync(message);
            stopwatch.Stop();

            Debug.WriteLine($"Time taken for the request : {stopwatch.ElapsedMilliseconds}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(responseBody))
                return Result.Success();
            else
                return Result.Failure("Something went wrong...");
        }
        catch (HttpRequestException e)
        {
            return Result.Failure<bool>(e.Message);
        }
    }

    public async Task<Result<IEnumerable<Budget>>> GetBudgets(int userId)
    {
        try
        {
            HttpRequestMessage message =
            new HttpRequestMessage(
                HttpMethod.Get,
                _httpClient.BaseAddress + $"/{userId}");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); // Запускаємо таймер
            using var response = await _httpClient.SendAsync(message); // Відправлення запиту на сервер
            stopwatch.Stop(); // Зупиняємо таймер після отримання відповіді
            Debug.WriteLine(
                $"Time taken for the request : {stopwatch.ElapsedMilliseconds}"); // Вивід результату в консоль
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var budgets = JsonConvert.DeserializeObject<IEnumerable<Budget>>(responseBody);

            return Result.Success<IEnumerable<Budget>>(budgets);
            
        }
        catch (HttpRequestException e)
        {
            return Result.Failure<IEnumerable<Budget>>(e.Message);
        }
        
    }
}
