using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.ViewModels
{
    [QueryProperty(nameof(User), "user")]
    public partial class AddCategoryViewModel : ObservableObject
    {
        private readonly IExpenseCategoryService _expenseCategory;
        private readonly IIncomeCategoryService _incomeCategory;

        [ObservableProperty]
        private User _user;

        [ObservableProperty]
        private int _selectedTransaction;

        [ObservableProperty]
        private string _selectedItem;


        [ObservableProperty]
        private string _name;

        [RelayCommand]
        public async Task Test()
        {
            int a = 5;
        }
        [RelayCommand]
        public async Task CreateCategory()
        {
            CSharpFunctionalExtensions.Result result;

            int a = 5;
            switch (_selectedTransaction)
            {

                case 0:
                    result = await _expenseCategory.CreateExpenseCategory(_user.Id,
                        new ExpenseCategory
                        {
                            Name = _name

                        });

                    break;

                case 1:
                    result = await _incomeCategory.CreateIncomeCategory(_user.Id,
                        new IncomeCategory
                        {
                            Name = _name

                        });
                    break;
                default:
                    return;

            }
            if (result.IsFailure)
            {
                await Shell.Current.DisplayAlert("", result.Error, "Ok");

            }
            else
                await Shell.Current.DisplayAlert("", "Категорію успішно створено.", "Ok");

        }

        public AddCategoryViewModel(IExpenseCategoryService expenseCategory, IIncomeCategoryService incomeCategory)
        {
            _expenseCategory = expenseCategory;
            _incomeCategory = incomeCategory;
        }
    }
}
