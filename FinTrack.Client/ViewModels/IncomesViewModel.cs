using CommunityToolkit.Mvvm.ComponentModel;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.ViewModels
{
    [QueryProperty(nameof(Budget), "budget")]
    public partial class IncomesViewModel : ObservableObject
    {
        private readonly IIncomeService _expenseService;


        [ObservableProperty]
        private Budget _budget;

        public ObservableCollection<Income> Incomes { get; private set; }

        public ObservableCollection<IncomeCategory> IncomeCategories { get; private set; } = new ObservableCollection<IncomeCategory>();

        public ObservableCollection<ISeries> Series { get; private set; }


        public IncomesViewModel(IIncomeService expenseService)
        {
            _expenseService = expenseService;

            //InitCollection();

            Task.Run(LoadData);
        }

        //private void InitCollection()
        //{
        //    _budget = new Budget()
        //    {
        //        Name = "Name",
        //        Currency = new Currency
        //        {
        //            Symbol = '$'
        //        }
        //    };
        //    var foodCategory = new IncomeCategory { Id = 1, Name = "Food" };
        //    var travelCategory = new IncomeCategory { Id = 2, Name = "Travel" };
        //    IncomeCategories.Add(foodCategory);
        //    IncomeCategories.Add(travelCategory);
        //    Incomes = new ObservableCollection<Income>
        //    {
        //        new Income
        //        {
        //            Id = 1,
        //            Name = "Grocery Shopping",
        //            Description = "Weekly groceries",
        //            IncomeVolume = 150.75m,
        //            IncomeDate = new DateTime(2024, 5, 25),
        //            IncomeCategoryId = foodCategory.Id,
        //            IncomeCategory = foodCategory
        //        },
        //        new Income
        //        {
        //            Id = 2,
        //            Name = "Taxi Ride",
        //            Description = "Ride to the airport",
        //            IncomeVolume = 50.00m,
        //            IncomeDate = new DateTime(2024, 5, 20),
        //            IncomeCategoryId = travelCategory.Id,
        //            IncomeCategory = travelCategory
        //        },
        //        new Income
        //        {
        //            Id = 3,
        //            Name = "Restaurant",
        //            Description = "Dinner with friends",
        //            IncomeVolume = 80.30m,
        //            IncomeDate = new DateTime(2024, 5, 22),
        //            IncomeCategoryId = foodCategory.Id,
        //            IncomeCategory = foodCategory
        //        }
        //    };
        //    Series = new ObservableCollection<ISeries>(IncomeCategories.Select(category =>
        //        new PieSeries<int>
        //        {
        //            Values = [Incomes.Count(expense => expense.IncomeCategory.Name == category.Name)],
        //            Name = category.Name
        //        }));





        //}
        private async Task LoadData()
        {
            while (true)
            {
                var result = await _expenseService.GetIncomes(_budget.Id);
                if (result.IsFailure)
                {
                    await Shell.Current.DisplayAlert("", result.Error, "Ok");
                }
                else
                {

                    Incomes = new ObservableCollection<Income>(result.Value);

                    Series = new ObservableCollection<ISeries>(
                        IncomeCategories.Select(category => new PieSeries<int>
                        {
                            Values = [
                                Incomes.Count(expense => expense.IncomeCategory.Name == category.Name)
                            ],
                            Name = category.Name
                        }));

                }

                await Task.Delay(30000);
            }
        }
    }
}
