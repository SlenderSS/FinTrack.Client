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




        [RelayCommand]
        public async Task CreateCategory()
        {

        }

        public AddCategoryViewModel(IExpenseCategoryService expenseCategory, IIncomeCategoryService incomeCategory)
        {
            _expenseCategory = expenseCategory;
            _incomeCategory = incomeCategory;
        }
    }
}
