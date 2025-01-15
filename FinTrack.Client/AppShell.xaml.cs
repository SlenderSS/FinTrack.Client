using FinTrack.Client.Pages;
using FinTrack.Client.Pages.Profile;

namespace FinTrack.Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("login/registration", typeof(RegistrationPage));
            
            Routing.RegisterRoute("budgets/createBudget", typeof(CreateBudgetView));
            Routing.RegisterRoute("budgets/budgetDetails", typeof(BudgetView));


            Routing.RegisterRoute("budgetDetails/expenses", typeof(ExpensesView));
            Routing.RegisterRoute("budgetDetails/incomes", typeof(IncomesView));

            Routing.RegisterRoute("budgetDetails/addCategory", typeof(AddCategoryView));
            Routing.RegisterRoute("budgetDetails/addExpense", typeof(AddExpenseView));
            Routing.RegisterRoute("budgetDetails/addIncome", typeof(AddIncomeView));

        }
    }
}
