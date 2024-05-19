using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinTrack.Client.ViewModels
{
    internal class RegistrationViewModel : ObservableObject
    {
        public string Message { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfPassword { get; set; }

        public ICommand RegistrationCommand => new Command(() =>
        {

        },
            () =>
            {
                if (Username == null || Password == null || ConfPassword == null)
                {
                    Message = "All data must be entered!";
                    return false;
                }
                if (Password != ConfPassword)
                {
                    Message = "Passwords dp not match!";
                    return false;
                }
                return true;
            }
            );

        public RegistrationViewModel()
        {

        }
    }
}
