using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using App1.Views;
using System.Threading.Tasks;

namespace App1.ViewModels
{
    internal class LoginViewModel
    {
        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }

        public LoginViewModel()
        {
            SignInCommand = new Command(async () => await SignIn());
            SignUpCommand = new Command(async () => await SignUp());
        }

        async Task SignIn()
        {
            await Shell.Current.GoToAsync("//main");
        }

        async Task SignUp()
        {
            await Shell.Current.GoToAsync("//login/registration");
        }
    }
}
