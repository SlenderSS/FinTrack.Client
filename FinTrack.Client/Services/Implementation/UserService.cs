using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinTrack.Client.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:44352/api/User";
        public UserService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }
        public async Task<Result<User>> Login(string username, string password)
        {
            try
            {
                HttpRequestMessage message =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    _httpClient.BaseAddress + $"?Name={username}&Password={password}");


                using var response = await _httpClient.SendAsync(message);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<User>(responseBody);

                return user;

            }
            catch (HttpRequestException e)
            {
                return Result.Failure<User>(e.Message);
            }
            

        }

        public async Task<Result> Registration(string name, string password)
        {
            try
            {
                HttpRequestMessage message =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    _httpClient.BaseAddress);
                var content = JsonConvert.SerializeObject( new { name, password } );

                message.Content = new StringContent( content, Encoding.UTF8, "application/json");

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

        public async Task<Result> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }


        public async Task<Result> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
