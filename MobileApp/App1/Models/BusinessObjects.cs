using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace App.Models
{
    public enum Plants
    {
        None,
        Carrot,
        Corn,
        Potato,
        Tomato,
        Wheat
    }

    public enum RecommendationTypes
    {
        None,
        Fertilizing,
        Harvest,
        Watering
    }

    public class CustomerInfo : ICustomerInfo
    {
        private List<Field> _fields = new();

        public event Action OnFieldsChanged;

        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        public List<Field> Fields
        {
            get => new(_fields);
            set
            {

                if (value is null) { throw new ArgumentNullException(nameof(value)); }
                if (value.Count > StaticSettings.ConfigVariables.FieldListLimitSize)
                {
                    throw new InvalidOperationException("List size limit exceeded.");
                }
                foreach (var field in value)
                {
                    if (field is null)
                    {
                        throw new ArgumentException("Some field is null.");
                    }
                }
                _fields = value;
                OnFieldsChanged?.Invoke();
            }
        }

        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        public void AddField(Field field)
        {
            if (!FieldAddingIsPossible)
            {
                throw new InvalidOperationException("List size limit exceeded.");
            }
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }
            _fields.Add(field);
            OnFieldsChanged?.Invoke();
        }

        public void DeleteField(Field field)
        {
            _fields.Remove(field);
            OnFieldsChanged?.Invoke();
        }

        private bool FieldAddingIsPossible => Fields.Count < StaticSettings.ConfigVariables.FieldListLimitSize;
    }

    public class Field : IField
    {
        private string _name;
        private double _latitude, _longitude;

        /// <exception cref="ArgumentException"/>
        public double Latitude
        {
            get => _latitude;
            set
            {
                if (value < -90 || 90 < value)
                {
                    throw new ArgumentException($"{nameof(value)} is out of valid interval."); 
                }
                _latitude = value;
            }
        }


        /// <exception cref="ArgumentException"/>
        public double Longitude
        {
            get => _longitude;
            set
            {
                if (value < -180 || 180 < value)
                {
                    throw new ArgumentException($"{nameof(value)} is out of valid interval.");
                }
                _longitude = value;
            }
        }

        [JsonIgnore]
        public Plants Plant { get; set; }
        public string PlantName
        {
            get => Enum.GetName(typeof(Plants), Plant);
            set
            {
                foreach (Plants plant in Enum.GetValues(typeof(Plants)))
                {
                    if (Enum.GetName(typeof(Plants), plant) == value)
                    {
                        Plant = plant;
                        return;
                    }
                }
                Plant = Plants.None;
            }
        }

        public long PlantingDate { get; set; }

        /// <exception cref="ArgumentException"/>
        public string Name
        {
            get => _name;
            set
            {
                if (!NameIsCorrect(value))
                {
                    throw new ArgumentException("Invalid Name value.");
                }
                _name = value;
            }
        }

        private static bool NameIsCorrect(string name) => name == null
                || (name.Length <= 50 && !new Regex(@"['""\\/\f\n\r\t]").IsMatch(name));
    }

    public class Recommendation : IRecommendation
    {
        [JsonIgnore]
        public RecommendationTypes Type { get; init; }
        public string TypeName
        {
            get => Enum.GetName(typeof(RecommendationTypes), Type);
            init
            {
                foreach (RecommendationTypes type in Enum.GetValues(typeof(RecommendationTypes)))
                {
                    if (Enum.GetName(typeof(RecommendationTypes), type) == value)
                    {
                        Type = type;
                        return;
                    }
                }
                Type = RecommendationTypes.None;
            }
        }
        public string Value { get; init; }
        public long RelevanceLimitTimestamp { get; init; }

        public bool IsRelevant => ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() <= RelevanceLimitTimestamp;
    }
}
