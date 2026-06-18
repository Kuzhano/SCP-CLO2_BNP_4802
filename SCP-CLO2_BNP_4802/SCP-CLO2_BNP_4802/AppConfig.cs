using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SCP_CLO2_BNP_4802
{
    public class AppConfig
    {
        public string ProposalFilePath { get; set; } = "proposals.json";

        public static AppConfig LoadConfiguration()
        {
            string configPath = "appconfig.json";
            if (File.Exists(configPath))
            {
                try
                {
                    string jsonString = File.ReadAllText(configPath);
                    return JsonSerializer.Deserialize<AppConfig>(jsonString) ?? new AppConfig();
                }
                catch (Exception)
                {
                    Console.WriteLine("Gagal membaca appconfig.json, menggunakan konfigurasi default.");
                }
            }
            return new AppConfig();
        }
    }
}
