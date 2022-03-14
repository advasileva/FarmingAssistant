using App.Models;
using App1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace App1.ViewModels
{
    internal class FieldsViewModel : BaseViewModel
    {
        private readonly Geocoder _geocoder = new();

        public ICommand CreateFieldCommand { get; }
        public ICommand UpdateFieldCommand { get; }
        public ICommand DeleteFieldCommand { get; }

        public FieldsViewModel()
        {
            CreateFieldCommand = new Command(async () => await CreateField(), () => !IsBusy);
            UpdateFieldCommand = new Command(async () => await UpdateField(), () => !IsBusy);
            DeleteFieldCommand = new Command(async () => await DeleteField(), () => !IsBusy);
            if (CurrentAccount.CustomerInfo.Fields.Count > 0)
            {
                SelectedField = CurrentAccount.CustomerInfo.Fields[0];
            }
        }

        public ObservableCollection<Field> BindingFields
        {
            get => new(CurrentAccount.CustomerInfo.Fields);
            set
            {
                OnPropertyChanged();
            }
        }

        private Field selectedField;
        public Field SelectedField
        {
            get => selectedField;
            set
            {
                if (selectedField != value)
                {
                    selectedField = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Plants> BindingPlants
        {
            get => new(Enum.GetValues(typeof(Plants)).Cast<Plants>());
            set
            {
                OnPropertyChanged();
            }
        }

        private async Task CreateField()
        {
            IsBusy = true;
            Task<Location> task = Geolocation.GetLastKnownLocationAsync();
            Location location = task.Result;
            CurrentAccount.CustomerInfo.AddField(new Field()
            {
                Plant = Plants.None,
                Name = "Новое поле",
                PlantingDate = DateTime.Now.Ticks,
                Longitude = location.Longitude,
                Latitude = location.Latitude,
            });
            await SaveChanges();
            SelectedField = CurrentAccount.CustomerInfo.Fields[^1];
            IsBusy = false;
        }

        private async Task UpdateField()
        {
            IsBusy = true;
            var remainedField = SelectedField.Name;
            await SaveChanges();
            SelectedField = BindingFields.FirstOrDefault(x => x.Name == remainedField);
            IsBusy = false;
        }

        private async Task DeleteField()
        {
            IsBusy = true;
            CurrentAccount.CustomerInfo.DeleteField(SelectedField);
            await SaveChanges();
            SelectedField = CurrentAccount.CustomerInfo.Fields.Count > 0 ? CurrentAccount.CustomerInfo.Fields[0] : null;
            IsBusy = false;
        }

        private async Task SaveChanges()
        {
            await DataStore.SaveAsync();
            await CurrentAccount.UpdateCustomerInfoAsync();
            BindingFields = new ObservableCollection<Field>(CurrentAccount.CustomerInfo.Fields);
        }

        public void OnMapChanged(System.Object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            var map = (Xamarin.Forms.Maps.Map)sender;
            map.Pins.Clear();
            map.Pins.Add(new Pin() { Label = "Position", Position = e.Position });
            map.MoveToRegion(MapSpan.FromCenterAndRadius(e.Position, map.VisibleRegion.Radius));
            SelectedField.Latitude = e.Position.Latitude;
            SelectedField.Longitude = e.Position.Longitude;
        }
    }
}
