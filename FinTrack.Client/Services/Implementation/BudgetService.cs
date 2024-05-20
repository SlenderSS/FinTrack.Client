using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
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
        private readonly string _baseUrl = "";
        public BudgetService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }
        public async Task<IEnumerable<Budget>> GetBudgets(int userId)
        {
            //try
            //{
            //    HttpRequestMessage message =
            //    new HttpRequestMessage(
            //        HttpMethod.Get,
            //        _httpClient.BaseAddress + $"?Name={username}&Password={password}");
            //    //var content = JsonConvert.SerializeObject( new { username, password } );
            //    using var response = await _httpClient.SendAsync(message);
            //    response.EnsureSuccessStatusCode();
            //    string responseBody = await response.Content.ReadAsStringAsync();
            //    if (int.TryParse(responseBody, out int result))
            //    {
            //        return Result.Success(result);
            //    }
            //    return Result.Failure<int>(responseBody);
            //}
            //catch (HttpRequestException e)
            //{
            //    return Result.Failure<int>(e.Message);
            //}
            return null;
        }
    }
}
