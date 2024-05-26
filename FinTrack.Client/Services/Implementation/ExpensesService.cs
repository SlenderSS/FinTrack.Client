using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.Services.Implementation
{
    public class ExpensesService : IExpenseService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44352/api/Expense"; // create https://localhost:44352/api/Expense?budgetId=1
        public ExpensesService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }
        public async Task<Result> CreateExpense(int budgetId, Expense expenseCreate)
        {
            try
            {
                HttpRequestMessage message =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    _httpClient.BaseAddress + $"?userId={budgetId}");
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


        public async Task<Result<IEnumerable<Expense>>> GetExpenses(int budgetId)
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

                var expenses = JsonConvert.DeserializeObject<IEnumerable<Expense>>(responseBody);

                return Result.Success<IEnumerable<Expense>>(expenses);

            }
            catch (HttpRequestException e)
            {
                return Result.Failure<IEnumerable<Expense>>(e.Message);
            }
        }
    }
}
