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
        }
    }
}
