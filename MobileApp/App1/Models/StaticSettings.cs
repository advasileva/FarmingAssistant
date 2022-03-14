using System.Text.Json;
using System.IO;

namespace App.Models
{
    public static class StaticSettings
    {
        public static readonly string ConfigFilePath = "config.json";
        public static readonly string AccountDataPath = "AccountData.json";
        public static readonly ConfigVariables DefaultConfigVariables = new()
        {
            ServerUrl = "farming-assistant.eastus.cloudapp.azure.com",
            ServerPort = 80,
            DefaultToken = "0000000000000000000000000000000000000000000000000000000000000000",
            SendingTimeout = 5000,
            ReceivingTimeout = 5000,
            FieldListLimitSize = 100,
            RecommendationStoreUpdatingPeriod = 600
        };

        static StaticSettings()
        {
            try
            {
                ConfigVariables = ConfigVariables.LoadFromFile();
            }
            catch
            {
                ConfigVariables = DefaultConfigVariables;
            }
        }

        public static ConfigVariables ConfigVariables { get; private set; }
    }

    public class ConfigVariables
    {
        public string ServerUrl { get; init; }
        public int ServerPort { get; init; }
        public int SendingTimeout { get; init; }
        public int ReceivingTimeout { get; init; }
        public string DefaultToken { get; init; }
        public int FieldListLimitSize { get; init; }
        public int RecommendationStoreUpdatingPeriod { get; init; }

        public static ConfigVariables LoadFromFile() =>
            JsonSerializer.Deserialize<ConfigVariables>(File.ReadAllText(StaticSettings.ConfigFilePath));
    }
}
