using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ModulPendaftaranProposal
{
    public class PendaftaranController<T> where T : class
    {
        private string _filePath;

        public PendaftaranController(string filePath)
        {
            _filePath = filePath;
        }

        public List<T> GetAllData()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            try
            {
                string jsonString = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal membaca data: {ex.Message}");
                return new List<T>();
            }
        }

        public void SaveData(T newData)
        {
            List<T> currentData = GetAllData();
            currentData.Add(newData);

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(currentData, options);
                File.WriteAllText(_filePath, jsonString);
                Console.WriteLine("Data proposal berhasil disimpan ke file JSON!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menyimpan data: {ex.Message}");
            }
        }
    }
}