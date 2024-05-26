using CSharpFunctionalExtensions;
using FinTrack.Client.Models;

namespace FinTrack.Client.Services.Interfaces
{
    public interface IIncomeService
    {
        Task<Result<IEnumerable<Income>>> GetIncomes(int budgetId);
        Task<Result> CreateIncome(int budgetId, Income incomeCreate);
    }
}
