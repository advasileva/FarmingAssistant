using System;
using System.Collections.Generic;

namespace App.Models
{
    public interface ICustomerInfo
    {
        event Action OnFieldsChanged;
        List<Field> Fields { get; set; }
        public void AddField(Field field);
        public void DeleteField(Field field);
    }

    public interface IField
    {
        string Name { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        Plants Plant { get; set; }
        long PlantingDate { get; set; }
    }

    public interface IRecommendation
    {
        RecommendationTypes Type { get; init; }
        string Value { get; init; }
    }
}