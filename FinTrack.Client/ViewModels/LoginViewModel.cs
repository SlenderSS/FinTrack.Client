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

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private User _user;


        [RelayCommand]
        public async Task Login()
        {

            if(string.IsNullOrEmpty(_name) || string.IsNullOrEmpty(Password))
            {
                await Shell.Current.DisplayAlert("", "All fields must be entered!", "Ok");
                return;
            }    
            var login = await _userService.Login(_name, Password);
            if (login.IsFailure)
            {
                await Shell.Current.DisplayAlert("", login.Error, "Ok");
                return;
            }
            else
            {
                await Shell.Current.DisplayAlert("", "Login successful", "Ok");
                _user = login.Value;
            }            
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"user", _user }
            };
            await Shell.Current.GoToAsync("//budgets", parameters);
        }


        [RelayCommand]
        public async Task Registration()
        { 
            await Shell.Current.GoToAsync("registration");
        }

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
