using CommunityToolkit.Mvvm.Input;
using FinTrack.Client.Services.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace FinTrack.Client.ViewModels;

public partial class RegistrationViewModel : ObservableObject
{
    private readonly IUserService _userService;

    [ObservableProperty]
    private bool _isHidden = !false;

    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _confPassword;


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
            await Shell.Current.DisplayAlert("Congratulation", regist.Error, "Ok");        
        else
            await Shell.Current.DisplayAlert("Congratulation", "Registration was successful", "Ok");

    }
    [RelayCommand]
    public async Task GoToLogin() => await Shell.Current.Navigation.PopAsync();

    public RegistrationViewModel(IUserService userService)
    {
        _userService = userService;
    }
}
