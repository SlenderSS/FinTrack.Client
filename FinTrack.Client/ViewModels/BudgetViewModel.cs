using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;

namespace FinTrack.Client.ViewModels;

[QueryProperty(nameof(Budget), "budget")]
public partial class BudgetViewModel : ObservableObject
{
    private readonly IBudgetService _budgetService;

    [ObservableProperty]
    private Budget _budget = new Budget {
        Name = "Test", 
        Currency = new Currency 
        { 
            Name ="USD", 
            Symbol = '$' 
        },
        CreationDate = DateTime.Now,
        PlannedAmountOfMoney = 10000,
        TotalAmountOfMoney = 1111
        
    };

    [RelayCommand]
    public async Task ShowExpenses()
    {
        var parameters = new Dictionary<string, object>
        {
            {"budget",  _budget}
        };

        await Shell.Current.GoToAsync("expenses", parameters);
    }

    [RelayCommand]
    public async Task ShowIncomes()
    {
        var parameters = new Dictionary<string, object>
        {
            {"budget",  _budget}
        };

        await Shell.Current.GoToAsync("incomes", parameters);
    }
    [RelayCommand]
    public async Task AddNewExpense()
    {
        var parameters = new Dictionary<string, object>
        {
            {"budget",  _budget}
        };

        await Shell.Current.GoToAsync("addExpense", parameters);
    }
    [RelayCommand]
    public async Task AddNewIncome()
    {
        var parameters = new Dictionary<string, object>
        {
            {"budget",  _budget}
        };

        await Shell.Current.GoToAsync("addIncome", parameters);
    }

    [RelayCommand]
    public async Task AddNewCategory()
    {
        var parameters = new Dictionary<string, object>
        {
            {"user",  _budget.User}
        };

        await Shell.Current.GoToAsync("addCategory", parameters);
    }

    public BudgetViewModel(IBudgetService budget)
    {
        _budgetService = budget;
    }
}
