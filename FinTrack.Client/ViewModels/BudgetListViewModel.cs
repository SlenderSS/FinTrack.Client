using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
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
    public partial class BudgetListViewModel : ObservableObject
    {
        private readonly IBudgetService _budgetService;
        private readonly ICurrencyService _currencyService;
        private readonly IPopupService _popupService;
        [ObservableProperty]
        private User _user;

        public ObservableCollection<Budget> Budgets { get; set; } = new ObservableCollection<Budget>();
        public ObservableCollection<Currency> Currencies { get; set; } = new ObservableCollection<Currency>();

        [ObservableProperty]
        private Budget _selectedBudget;

        [ObservableProperty]
        private Budget _budgetCreate;


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
            var parameners = new Dictionary<string, object>()
            {
                { "budget", _selectedBudget }
            };

            await Shell.Current.GoToAsync("budgetDetails", parameners);
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            var result = await _budgetService.GetBudgets(User.Id);
            var currencies = await _currencyService.GetCurrencies();

            if (currencies.IsFailure)
                await Shell.Current.DisplayAlert("Error", currencies.Error, "Ok");
            else if (result.IsFailure)
                await Shell.Current.DisplayAlert("Error", result.Error, "Ok");
            else
            {
                Budgets = new ObservableCollection<Budget>(result.Value);
                Currencies = new ObservableCollection<Currency>(currencies.Value);
            }

        }
        public BudgetListViewModel(IBudgetService budgetService, 
            ICurrencyService currencyService, 
            IPopupService popupService)
        {
            _budgetService = budgetService;
            _currencyService = currencyService;
            _popupService = popupService;


            //Task.Run(LoadDataAsync);
            InitCollection();
        }

        private void InitCollection()
        {
            Budgets = new ObservableCollection<Budget>
        {
            new Budget
            {
                Id = 1,
                Name = "Budget 1",
                PlannedAmountOfMoney = 1000.00m,
                TotalAmountOfMoney = 1200.00m,
                UserId = 1,
                User = new User { Id = 1, Name = "User 1" },
                CreationDate = DateTime.Now.AddMonths(-1),
                CurrencyId = 1,
                Currency = new Currency { Id = 1, Name = "USD" }
            },
            new Budget
            {
                Id = 2,
                Name = "Budget 2",
                PlannedAmountOfMoney = 2000.00m,
                TotalAmountOfMoney = 1800.00m,
                UserId = 2,
                User = new User { Id = 2, Name = "User 2" },
                CreationDate = DateTime.Now.AddMonths(-2),
                CurrencyId = 2,
                Currency = new Currency { Id = 2, Name = "EUR" }
            },
            new Budget
            {
                Id = 3,
                Name = "Budget 3",
                PlannedAmountOfMoney = 1500.00m,
                TotalAmountOfMoney = 1500.00m,
                UserId = 1,
                User = new User { Id = 1, Name = "User 1" },
                CreationDate = DateTime.Now.AddMonths(-3),
                CurrencyId = 3,
                Currency = new Currency { Id = 3, Name = "GBP" }
            },
            new Budget
            {
                Id = 1,
                Name = "Budget 1",
                PlannedAmountOfMoney = 1000.00m,
                TotalAmountOfMoney = 1200.00m,
                UserId = 1,
                User = new User { Id = 1, Name = "User 1" },
                CreationDate = DateTime.Now.AddMonths(-1),
                CurrencyId = 1,
                Currency = new Currency { Id = 1, Name = "USD" }
            },
            new Budget
            {
                Id = 2,
                Name = "Budget 2",
                PlannedAmountOfMoney = 2000.00m,
                TotalAmountOfMoney = 1800.00m,
                UserId = 2,
                User = new User { Id = 2, Name = "User 2" },
                CreationDate = DateTime.Now.AddMonths(-2),
                CurrencyId = 2,
                Currency = new Currency { Id = 2, Name = "EUR" }
            },
            new Budget
            {
                Id = 3,
                Name = "Budget 3",
                PlannedAmountOfMoney = 1500.00m,
                TotalAmountOfMoney = 1500.00m,
                UserId = 1,
                User = new User { Id = 1, Name = "User 1" },
                CreationDate = DateTime.Now.AddMonths(-3),
                CurrencyId = 3,
                Currency = new Currency { Id = 3, Name = "GBP" }
            }
        };
        }
    }
}
