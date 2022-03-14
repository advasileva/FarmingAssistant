using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using App.Models;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldsPage : ContentPage
    {
        public FieldsPage()
        {
            InitializeComponent();
            FieldMap.MapClicked += (BindingContext as FieldsViewModel).OnMapChanged;
            try
            {
                Task<Location> task = Geolocation.GetLastKnownLocationAsync();
                Location location = task.Result;
                Position position = new(location.Latitude, location.Longitude);
                //Field field = (FieldsPicker.SelectedItem as Field);
                //Position position = new(field.Latitude, field.Longitude);
                FieldMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(10000)));
                FieldMap.Pins.Add(new Pin() { Label = "Position", Position = position });
            } 
            catch { }
        }
    }
}