using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using System.Collections.ObjectModel;

namespace FinTrack.Client.ViewModels;

[QueryProperty(nameof(User), "user")]
public partial class BudgetCreateViewModel : ObservableObject
{
    private readonly IBudgetService _budgetService;
    private readonly ICurrencyService _currencyService;

    [ObservableProperty]
    private User _user;

    private ObservableCollection<Currency> currencies;

    public ObservableCollection<Currency> Currencies
    {
        get => currencies; set
        {
            currencies = value;
            OnPropertyChanged();
        }
    }

    [ObservableProperty]
    private Currency _budgetCurrency;

    [ObservableProperty]
    private decimal _budgetAmount;

    [ObservableProperty]
    private string _budgetTitle;

    public BudgetCreateViewModel(IBudgetService budgetService, ICurrencyService currencyService)
    {
        _budgetService = budgetService;
        _currencyService = currencyService;

        //GenerateCurrencies();

        Task.Run(async () =>
        {
            while (true)
            {
                var result = await _currencyService.GetCurrencies();
                Currencies = new ObservableCollection<Currency>(result.Value);

                await Task.Delay(30000);
            }
        });
    }
  
    [RelayCommand]
    public async Task CreateBudget()
    {
        if (_budgetAmount == 0 || string.IsNullOrWhiteSpace(_budgetTitle))
        {
            await Shell.Current.DisplayAlert("", "All members must be entered!", "Ok");
            return;
        }
        var result = await _budgetService.CreateBudget(_user.Id, _budgetCurrency.Id, new Budget()
        {
            Name = _budgetTitle,
            User = _user,
            UserId = _user.Id,
            PlannedAmountOfMoney = _budgetAmount,
            TotalAmountOfMoney = _budgetAmount,
            CreationDate = DateTime.Now,
            Currency = _budgetCurrency,
            CurrencyId = _budgetCurrency.Id
        });
        if (result.IsFailure)
        {
            await Shell.Current.DisplayAlert("", result.Error, "Ok");
        }
        else
        {
            await Shell.Current.DisplayAlert("", "New budget created successful!", "Ok");
        }
    }
}
