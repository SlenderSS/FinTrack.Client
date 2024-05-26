using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class BudgetViewModel : ObservableObject
    {
        private readonly IBudgetService budget;


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

        public BudgetViewModel(IBudgetService budget)
        {
            this.budget = budget;
        }
    }
}
