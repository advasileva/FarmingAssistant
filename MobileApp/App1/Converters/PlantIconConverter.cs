using App.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace App1.Converters
{
    public class PlantIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return PlantImages[(Plants)value];
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public readonly Dictionary<Plants, string> PlantImages = new()
        {
            [Plants.Carrot] = "carrot.png",
            [Plants.Potato] = "potatoes.png",
            [Plants.Wheat] = "grass.png",
            [Plants.Tomato] = "tomato.png",
            [Plants.Corn] = "corn.png",
            [Plants.None] = "default.png"
        };
    }
}