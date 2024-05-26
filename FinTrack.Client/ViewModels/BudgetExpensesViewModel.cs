using CommunityToolkit.Mvvm.ComponentModel;
using FinTrack.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Client.ViewModels
{
    [QueryProperty(nameof(Budget), "budgetId")]
    public class BudgetExpensesViewModel : ObservableObject
    {


        public BudgetExpensesViewModel()
        {
            
        }
    }
}
