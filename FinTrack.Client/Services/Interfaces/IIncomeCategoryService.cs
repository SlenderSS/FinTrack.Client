using CSharpFunctionalExtensions;
using FinTrack.Client.Models;

namespace FinTrack.Client.Services.Interfaces
{
    interface IIncomeCategoryService
    {
        Task<Result<IEnumerable<IncomeCategory>>> GetIncomeCategories(int budgetId);
        Task<Result> CreateIncomeCategory(int budgetId, IncomeCategory categoryCreate);
    }
}
