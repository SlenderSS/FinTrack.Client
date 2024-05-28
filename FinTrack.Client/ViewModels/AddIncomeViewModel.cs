using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Client.Models;
using FinTrack.Client.Services.Interfaces;
using System.Collections.ObjectModel;

namespace FinTrack.Client.ViewModels
{
    [QueryProperty(nameof(Budget), "budget")]
    public partial class AddIncomeViewModel : ObservableObject
    {
        private readonly IIncomeService _incomeService;
        private readonly IIncomeCategoryService _categoryService;

        public ObservableCollection<IncomeCategory> IncomeCategories { get; set; }


        [ObservableProperty]
        private IncomeCategory _incomeCategory;

        [ObservableProperty]
        private Budget _budget;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private decimal _incomeVolume;


        [RelayCommand]
        public async Task CreateIncome()
        {
            if (string.IsNullOrWhiteSpace(_name) ||
                _incomeVolume == 0 ||
                IncomeCategory == null)
                return;

            var created = await _incomeService.CreateIncome(_budget.Id, new Income
            {
                Name = _name,
                Description = _description,
                IncomeVolume = _incomeVolume,
                IncomeCategoryId = _incomeCategory.Id
            });

            if (created.IsFailure)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Error", created.Error, "Ok");
            }
            else
            {
                await Shell.Current.CurrentPage.DisplayAlert("Success", "The income was successfully added!", "Ok");
            }
        }


        public AddIncomeViewModel(IIncomeService incomeService, IIncomeCategoryService categoryService)
        {
            _incomeService = incomeService;
            _categoryService = categoryService;

            Task.Run(async () =>
            {
                while (true)
                {
                    var categories = await _categoryService.GetIncomeCategories(Budget.UserId);
                    IncomeCategories = new ObservableCollection<IncomeCategory>(categories.Value);

                    await Task.Delay(30000);
                }
            });
        }
    }
}
