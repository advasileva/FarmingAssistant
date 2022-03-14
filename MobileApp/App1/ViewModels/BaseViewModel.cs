using Acr.UserDialogs;
using App.Models;
using App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            CurrentAccount = new List<Account>(DataStore.GetItemsAsync().Result)[0];
        }
        public IDataStore<Account> DataStore { get; set; } = DependencyService.Get<IDataStore<Account>>();

        public Account CurrentAccount { get; set; }

        protected void ShowToast(string message)
        {
            ToastConfig toastConfig = new(message);
            toastConfig.SetDuration(2000);
            toastConfig.SetBackgroundColor(Color.FromHex("#B1B4FF"));
            toastConfig.SetMessageTextColor(System.Drawing.Color.DarkSlateBlue);
            toastConfig.SetPosition(ToastPosition.Top);
            UserDialogs.Instance.Toast(toastConfig);
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}