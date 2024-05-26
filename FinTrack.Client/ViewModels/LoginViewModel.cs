using FinTrack.Client.Services.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using FinTrack.Client.Models;

namespace FinTrack.Client.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IUserService _userService;
        private string _login;
        private string _password;
        private string _message;
        private User _user;
        public string Login { get => _login; set => SetProperty(ref _login, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string Message { get => _message; set => SetProperty(ref _message, value); }


        public ICommand LoginCommand => new Command(async () =>
        {

            if(string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                await AppShell.Current.DisplayAlert("", "All fields must be entered!", "Ok");
                return;
            }    
            var login = await _userService.Login(Login, Password);
            if (login.IsFailure)
            {
                Message = login.Error;
            }
            else
            {
                Message = "Login successed";
                _user = login.Value;
            }
            await AppShell.Current.DisplayAlert("", "Login successed", "Ok");
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"user", _user }
            };
            await Shell.Current.GoToAsync("//budgets", parameters);
        });


        [RelayCommand]
        public async Task RegistrationCommand()
        { 
            await Shell.Current.GoToAsync("registration");
        }

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
