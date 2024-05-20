using CSharpFunctionalExtensions;
using FinTrack.Client.Models;

namespace FinTrack.Client.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<Result<IEnumerable<Currency>>> GetCurrencies();
    }
}