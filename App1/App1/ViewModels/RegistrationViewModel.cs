using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
    internal class RegistrationViewModel
    {
        public ICommand RegisterCommand { get; }

        public RegistrationViewModel()
        {
            RegisterCommand = new Command(async () => await Register());
        }

        async Task Register()
        {
            await Shell.Current.GoToAsync("//main");
        }
    }
}
