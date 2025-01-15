using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using System.Collections.ObjectModel;

namespace FinTrack.Client.ViewModels;

[QueryProperty(nameof(Budget), "budget")]
public partial class AddExpenseViewModel : ObservableObject
{
    private readonly IExpenseService _expenseService;
    private readonly IExpenseCategoryService _categoryService;

    private ObservableCollection<ExpenseCategory> _expenseCategories;

    public ObservableCollection<ExpenseCategory> ExpenseCategories
    {
        get => _expenseCategories;
        set
        {
            _expenseCategories = value;
            OnPropertyChanged();
        }
    }

    [ObservableProperty]
    private ExpenseCategory _expenseCategory;

    [ObservableProperty]
    private Budget _budget;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private decimal _expenseVolume;


    [RelayCommand]
    public async Task CreateExpense()
    {

        if (string.IsNullOrWhiteSpace(_name) ||
            _expenseVolume == 0 ||
            ExpenseCategory == null)
            return;

        var created = await _expenseService.CreateExpense(_budget.Id, new Expense
        {
            Name = _name,
            Description = _description,
            ExpenseVolume = _expenseVolume,
            ExpenseCategoryId = _expenseCategory.Id
        });

        if (created.IsFailure)
        {
            await Shell.Current.CurrentPage.DisplayAlert("Error", created.Error, "Ok");
        }
        else
        {
            await Shell.Current.CurrentPage.DisplayAlert("Success", "The expense was successfully added!", "Ok");
        }

    }

    public AddExpenseViewModel(IExpenseService expenseService, IExpenseCategoryService categoryService)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;

        Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(1000);
                var result = await _categoryService.GetExpenseCategories(Budget.User.Id);
                var categories = result.Value;
                ExpenseCategories = new ObservableCollection<ExpenseCategory>(categories);

                await Task.Delay(30000);
            }
        });
    }
}
