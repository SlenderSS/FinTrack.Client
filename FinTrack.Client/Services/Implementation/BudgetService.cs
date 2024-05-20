using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.Services.Implementation
{
    public class BudgetService : IBudgetService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44352/api/Budget"; // https://localhost:44352/api/Budget?userId=1&currencyId=1
        public BudgetService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public Task<Result> CreateBudget(Budget budget)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<Budget>>> GetBudgets(int userId)
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

                var budgets = JsonConvert.DeserializeObject<IEnumerable<Budget>>(responseBody);

                return Result.Success<IEnumerable<Budget>>(budgets);
                
            }
            catch (HttpRequestException e)
            {
                return Result.Failure<IEnumerable<Budget>>(e.Message);
            }
            
        }
    }
}
