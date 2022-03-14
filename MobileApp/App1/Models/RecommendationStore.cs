using System;
using System.Collections.Generic;

namespace App.Models
{
    public class RecommendationStore
    {
        private readonly Dictionary<Field, Recommendation[]> _recommendations = new();
        private readonly Dictionary<Field, long> _lastUpdates = new();
        private readonly object _lockObject = new();

        public Recommendation[] GetRecommendations(Field field)
        {
            lock (_lockObject)
            {
                if (_recommendations.ContainsKey(field))
                {
                    return _recommendations[field];
                }
                return Array.Empty<Recommendation>();
            }
        }

        public void AddRecommendations(Field field, Recommendation[] recommendations)
        {
            lock (_lockObject)
            {
                _recommendations.Remove(field);
                _recommendations.Add(field, recommendations);
                _lastUpdates.Remove(field);
                _lastUpdates.Add(field, GetCurrentTimestamp());
            }
        }

        public bool ShouldLoad(Field field)
        {
            lock (_lockObject)
            {
                if (!_recommendations.ContainsKey(field) || !_lastUpdates.ContainsKey(field))
                {
                    return true;
                }
                foreach (var recommendation in _recommendations[field])
                {
                    if (!recommendation.IsRelevant)
                    {
                        return true;
                    }
                }
                if (GetCurrentTimestamp() - _lastUpdates[field]
                        > StaticSettings.ConfigVariables.RecommendationStoreUpdatingPeriod)
                {
                    return true;
                }
            }
            return false;
        }

        private static long GetCurrentTimestamp() => ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
    }
}
