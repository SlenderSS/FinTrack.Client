using FinTrack.Client.Pages;

namespace FinTrack.Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("login/registration", typeof(RegistrationPage));
        }
    }
}
