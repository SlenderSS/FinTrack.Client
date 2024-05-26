using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.ViewModels
{
    [QueryProperty(nameof(Budget), "budget")]
    public partial class AddExpenseViewModel : ObservableObject
    {
        private readonly IExpenseService _expenseService;
        private readonly IExpenseCategoryService _categoryService;

        public ObservableCollection<ExpenseCategory> ExpenseCategories { get; set; }


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
        public async Task CreateTransaction()
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
                    var categories = await _categoryService.GetExpenseCategories(Budget.UserId);
                    ExpenseCategories = new ObservableCollection<ExpenseCategory>(categories.Value);

                    await Task.Delay(30000);
                }
            });
        }
    }
}
