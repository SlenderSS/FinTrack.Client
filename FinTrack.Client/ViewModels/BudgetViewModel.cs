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
        public async Task AddNewExpense()
        {
            var parameters = new Dictionary<string, object>
            {
                {"budget",  _budget}
            };

            await Shell.Current.GoToAsync("addExpense", parameters);
        }

        public BudgetViewModel(IBudgetService budget)
        {
            _budgetService = budget;
        }
    }
}
