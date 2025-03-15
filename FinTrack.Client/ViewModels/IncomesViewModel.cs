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
        private readonly IIncomeCategoryService _categoryService;
        [ObservableProperty]
        private Budget _budget;
        private ObservableCollection<Income> incomes;
        private ObservableCollection<ISeries> series;

        public ObservableCollection<Income> Incomes
        {
            get => incomes; private set
            {
                incomes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IncomeCategory> IncomeCategories { get; private set; } = new ObservableCollection<IncomeCategory>();

        public ObservableCollection<ISeries> Series
        {
            get => series; private set
            {
                series = value;
                OnPropertyChanged();
            }
        }


        public IncomesViewModel(IIncomeService expenseService, IIncomeCategoryService categoryService)
        {
            _expenseService = expenseService;
            _categoryService = categoryService;

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
            await Task.Delay(500);
            while (true)
            {
                var categoriesResult = await _categoryService.GetIncomeCategories(Budget.UserId);
                if (categoriesResult.IsFailure)
                {
                    await Shell.Current.DisplayAlert("", categoriesResult.Error, "Ok");
                    continue;
                }
                IncomeCategories = new ObservableCollection<IncomeCategory>(categoriesResult.Value);
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
                                Incomes.Count(income => income.IncomeCategory.Name == category.Name)
                            ],
                            Name = category.Name
                        }));

                }

                await Task.Delay(30000);
            }
        }
    }
}
