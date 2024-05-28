using CommunityToolkit.Mvvm.ComponentModel;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Maui;
using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using CommunityToolkit.Maui.Core.Extensions;
using LiveChartsCore.SkiaSharpView;



namespace FinTrack.Client.ViewModels
{
    [QueryProperty(nameof(Budget), "budget")]
    public partial class ExpensesViewModel : ObservableObject
    {
        private readonly IExpenseService _expenseService;


        [ObservableProperty]
        private Budget _budget;

        public ObservableCollection<Expense> Expenses { get; private set; }

        public ObservableCollection<ExpenseCategory> ExpenseCategories { get; private set; } = new ObservableCollection<ExpenseCategory>();





        public ObservableCollection<ISeries> Series { get; private set; }





        public ExpensesViewModel(IExpenseService expenseService)
        {
            _expenseService = expenseService;

            InitCollection();

            //Task.Run(LoadData);
        }

        private void InitCollection()
        {
            _budget = new Budget()
            { Name = "Name",
                Currency = new Currency
                {
                    Symbol = '$'
                }
            };
            var foodCategory = new ExpenseCategory { Id = 1, Name = "Food" };
            var travelCategory = new ExpenseCategory { Id = 2, Name = "Travel" };
            ExpenseCategories.Add(foodCategory);
            ExpenseCategories.Add(travelCategory);
            Expenses = new ObservableCollection<Expense>
            {
                new Expense
                {
                    Id = 1,
                    Name = "Grocery Shopping",
                    Description = "Weekly groceries",
                    ExpenseVolume = 150.75m,
                    ExpenseDate = new DateTime(2024, 5, 25),
                    ExpenseCategoryId = foodCategory.Id,
                    ExpenseCategory = foodCategory
                },
                new Expense
                {
                    Id = 2,
                    Name = "Taxi Ride",
                    Description = "Ride to the airport",
                    ExpenseVolume = 50.00m,
                    ExpenseDate = new DateTime(2024, 5, 20),
                    ExpenseCategoryId = travelCategory.Id,
                    ExpenseCategory = travelCategory
                },
                new Expense
                {
                    Id = 3,
                    Name = "Restaurant",
                    Description = "Dinner with friends",
                    ExpenseVolume = 80.30m,
                    ExpenseDate = new DateTime(2024, 5, 22),
                    ExpenseCategoryId = foodCategory.Id,
                    ExpenseCategory = foodCategory
                }
            };
            Series = new ObservableCollection<ISeries>(ExpenseCategories.Select(category =>
                new PieSeries<int>
                {
                    Values = [Expenses.Count(expense => expense.ExpenseCategory.Name == category.Name)],
                    Name = category.Name
                }));





        }
        private async Task LoadData()
        {
            while (true)
            {
                var result = await _expenseService.GetExpenses(_budget.Id);
                if (result.IsFailure)
                {
                    await Shell.Current.DisplayAlert("", result.Error, "Ok");
                }
                else
                {

                    Expenses = new ObservableCollection<Expense>(result.Value);

                    Series = new ObservableCollection<ISeries>(
                        ExpenseCategories.Select(category => new PieSeries<int>
                        {
                            Values = [
                                Expenses.Count(expense => expense.ExpenseCategory.Name == category.Name)
                            ],
                            Name = category.Name
                        }));

                }

                await Task.Delay(30000);
            }
        }
    }
}
