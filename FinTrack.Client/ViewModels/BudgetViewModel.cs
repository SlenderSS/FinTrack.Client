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
    [QueryProperty(nameof(User), "user")]
    public partial class BudgetViewModel : ObservableObject
    {
        private readonly IBudgetService _budgetService;
        private readonly ICurrencyService _currencyService;


        [ObservableProperty]
        private User _user;

        public ObservableCollection<Budget> Budgets { get; set; }
        public ObservableCollection<Currency> Currencies { get; set; }

        [ObservableProperty]
        private Budget _selectedBudget;

        [ObservableProperty]
        private Budget _budgetCreate;


        [RelayCommand]
        public async Task CreateBudget()
        {

        }
        public BudgetViewModel(IBudgetService budgetService, ICurrencyService currencyService)   
        {
            this._budgetService = budgetService;
            this._currencyService = currencyService;

        }
    }
}
