
using App1.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App1
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("registration", typeof(RegistrationPage));
        }

    }
}
