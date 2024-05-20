using CSharpFunctionalExtensions;
using FinTrack.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<Result<IEnumerable<Budget>>> GetBudgets(int userId);
        Task<Result> CreateBudget(Budget budget);


    }
}
