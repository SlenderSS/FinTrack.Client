using FinTrack.Client.Services.Implementation;
using FinTrack.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.Services
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IBudgetService, BudgetService>()
            .AddTransient<ICurrencyService, CurrencyService>()
            .AddTransient<IExpenseCategoryService, ExpenseCategoriesService>()
            .AddTransient<IIncomeCategoryService, IncomeCategoriesService>()
            .AddTransient<IExpenseService, ExpensesService>()
            .AddTransient<IIncomeService, IncomesService>()
            .AddTransient<IUserService, UserService>();
    }
}
