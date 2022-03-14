using App1.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1.ViewModels;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        { 
            InitializeComponent();
            loginIcon.Source = ImageSource.FromResource("App1.Icons.LoginIcon.png");
            Appearing += (BindingContext as LoginViewModel).PageAppearing;
        }
    }
}