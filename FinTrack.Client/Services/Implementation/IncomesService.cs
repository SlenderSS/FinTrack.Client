using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace FinTrack.Client.Services.Implementation
{
    public class IncomesService : IIncomeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44352/api/Income";
        public IncomesService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }
        public async Task<Result> CreateIncome(int budgetId, Income incomeCreate)
        {
            try
            {
                HttpRequestMessage message =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    _httpClient.BaseAddress + $"?budgetId={budgetId}");
                var content = JsonConvert.SerializeObject(incomeCreate);

                message.Content = new StringContent(content, Encoding.UTF8, "application/json");

                using var response = await _httpClient.SendAsync(message);

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

        public async Task<Result<IEnumerable<Income>>> GetIncomes(int budgetId)
        {
            try
            {
                HttpRequestMessage message =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    _httpClient.BaseAddress + $"/{budgetId}");

                using var response = await _httpClient.SendAsync(message);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var incomes = JsonConvert.DeserializeObject<IEnumerable<Income>>(responseBody);

                return Result.Success<IEnumerable<Income>>(incomes);

            }
            catch (HttpRequestException e)
            {
                return Result.Failure<IEnumerable<Income>>(e.Message);
            }
        }
    }
}
