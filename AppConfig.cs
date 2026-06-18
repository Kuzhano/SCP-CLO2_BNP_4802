using System;
using System.IO;
using System.Text.Json;

namespace DeLFINA_CLI
{
    public class AppConfig
    {
        public string ProposalFilePath { get; set; } = "proposals.json";
        public string DashboardJudul { get; set; } = "DASHBOARD PEMANTAUAN PROPOSAL";
        public int DashboardMaxBaris { get; set; } = 10;

        public static AppConfig LoadConfiguration()
        {
            string configPath = "appconfig.json";
            if (!File.Exists(configPath))
            {
                var defaultConfig = new AppConfig();
                File.WriteAllText(configPath, JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true }));
                return defaultConfig;
            }

            try
            {
                return JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(configPath)) ?? new AppConfig();
            }
            catch (JsonException)
            {
                return new AppConfig();
            }
        }
    }
}