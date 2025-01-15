using CSharpFunctionalExtensions;
using FinTrack.Client.Models;

namespace FinTrack.Client.Services.Interfaces
{
    public interface IExpenseCategoryService
    {
        Task<Result<IEnumerable<ExpenseCategory>>> GetExpenseCategories(int budgetId);
        Task<Result> CreateExpenseCategory(int userId, ExpenseCategory categoryCreate);
    }
}
