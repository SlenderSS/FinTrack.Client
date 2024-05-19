using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinTrack.Client.ViewModels
{
    internal class LoginViewModel : ObservableObject
    {

        public string Login { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand => new Command(() =>
        {

        });


        public LoginViewModel()
        {
            
        }
    }
}
