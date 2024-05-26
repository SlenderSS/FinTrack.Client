using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<Result<IEnumerable<Expense>>> GetExpenses(int userId);
        Task<Result> CreateExpense(int userId, Expense categoryCreate);
    }
}
