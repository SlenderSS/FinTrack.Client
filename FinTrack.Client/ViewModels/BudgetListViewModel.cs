using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using System.Collections.ObjectModel;

namespace FinTrack.Client.ViewModels;

[QueryProperty(nameof(User), "user")]
public partial class BudgetListViewModel : ObservableObject
{
    private readonly IBudgetService _budgetService;
    private readonly ICurrencyService _currencyService;

    private ObservableCollection<Budget> budgets;



    [ObservableProperty]
    private User _user;

    public ObservableCollection<Budget> Budgets
    {
        get => budgets; set
        {
            budgets = value;
            OnPropertyChanged();
        }
    }

    [ObservableProperty]
    private Budget _selectedBudget;

    [RelayCommand]
    public async Task CreateBudget()
    {
        var parameners = new Dictionary<string, object>()
        {
            { "user", _user }
        };

        await Shell.Current.GoToAsync("createBudget", parameners);
    }

    [RelayCommand]
    public async Task GetBudgetDetails()
    {
        _selectedBudget.User = _user;
        var parameners = new Dictionary<string, object>()
        {
            { "budget", _selectedBudget }
        };

        await Shell.Current.GoToAsync("budgetDetails", parameners);
    }

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        await Task.Delay(500);

        while (true)
        {
            var result = await _budgetService.GetBudgets(User.Id);

            if (result.IsFailure)
                await Shell.Current.DisplayAlert("Error", result.Error, "Ok");
            else
                Budgets = new ObservableCollection<Budget>(result.Value);

            await Task.Delay(30000);
        }
    }
    public BudgetListViewModel(IBudgetService budgetService,
        ICurrencyService currencyService)
    {
        _budgetService = budgetService;
        _currencyService = currencyService;

        Task.Run(LoadDataAsync);

    }
}
