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
        Task<IEnumerable<Budget>> GetBudgets(int userId);
    }
}
