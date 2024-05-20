using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;

namespace FinTrack.Client.Services.Implementation
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44352/api/Currency";
        public CurrencyService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<Result<IEnumerable<Currency>>> GetCurrencies()
        {
            try
            {
                HttpRequestMessage message =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    _httpClient.BaseAddress);   
                
                using var response = await _httpClient.SendAsync(message);
                response.EnsureSuccessStatusCode();
                if(response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var currencies = JsonConvert.DeserializeObject<IEnumerable<Currency>>(responseBody);
                    return Result.Success<IEnumerable<Currency>>(currencies);
                }
                else
                    return Result.Failure<IEnumerable<Currency>>("Something went wrong...");
            }
            catch (HttpRequestException e)
            {
                return Result.Failure<IEnumerable<Currency>>(e.Message);
            }
        }
    }
}
