using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace FinTrack.Client.Services.Implementation
{
    public class IncomeCategoriesService : IIncomeCategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44352/api/IncomeCategory"; // create https://localhost:44352/api/Income?budgetId=1
        public IncomeCategoriesService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }
        public async Task<Result> CreateIncomeCategory(int userId, IncomeCategory expenseCreate)
        {
            try
            {
                HttpRequestMessage message =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    _httpClient.BaseAddress + $"?userId={userId}");
                var content = JsonConvert.SerializeObject(expenseCreate);

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


        public async Task<Result<IEnumerable<IncomeCategory>>> GetIncomeCategories(int userId)
        {
            try
            {
                HttpRequestMessage message =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    _httpClient.BaseAddress + $"/{userId}");

                using var response = await _httpClient.SendAsync(message);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var expenses = JsonConvert.DeserializeObject<IEnumerable<IncomeCategory>>(responseBody);

                return Result.Success<IEnumerable<IncomeCategory>>(expenses);

            }
            catch (HttpRequestException e)
            {
                return Result.Failure<IEnumerable<IncomeCategory>>(e.Message);
            }
        }


    }
}
