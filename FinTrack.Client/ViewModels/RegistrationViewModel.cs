using CommunityToolkit.Mvvm.Input;
using FinTrack.Client.Services.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinTrack.Client.ViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        private bool isHidden = true;
        private string message;
        private string username;
        private string password;
        private string confPassword;
        private readonly IUserService _userService;

        public string Message { get => message; set => SetProperty(ref message, value); }
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string ConfPassword { get => confPassword; set => SetProperty(ref confPassword, value); }
        public bool IsHidden { get => isHidden; set => SetProperty(ref isHidden, value); }

        [RelayCommand]
        public async Task Registration()
        {
            if (Username == null || Password == null || ConfPassword == null)
            {
                await Shell.Current.DisplayAlert("Congratulation", "All data must be entered!", "Ok");
                return;
            }
            if (Password != ConfPassword)
            {
               
                await Shell.Current.DisplayAlert("Congratulation", "Passwords do not match!", "Ok");
                return;
            }
            
            var regist = await _userService.Registration(Username, Password);
            if (regist.IsFailure)
            {
                await Shell.Current.DisplayAlert("Congratulation", regist.Error, "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Congratulation", "Registration was successful", "Ok");

            }
        }
        [RelayCommand]
        public async Task GoToLogin()
        {
            await Shell.Current.Navigation.PopAsync();
        }





        public RegistrationViewModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
