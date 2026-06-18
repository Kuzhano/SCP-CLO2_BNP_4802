// ==========================================
// API (Internal Contract)
// ==========================================
using System.Text.Json;

﻿public class UserAccount
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // "Admin" atau "Dosen"
}

public interface IAuthApi
{
    UserAccount Login(string username, string password);
}

public class JsonAuthService : IAuthApi
{
    private readonly string _dbPath;

    public JsonAuthService(string dbPath)
    {
        // DbC: Pre-condition (Memastikan path tidak null/kosong)
        if (string.IsNullOrWhiteSpace(dbPath))
            throw new ArgumentException("Path database tidak boleh kosong.");
        _dbPath = dbPath;
    }

    public UserAccount Login(string username, string password)
    {
        // DbC: Pre-condition (Validasi input)
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("[!] Error: Username dan Password harus diisi.");
            return null;
        }

        // DbC: Defensive Programming (Mencegah crash jika file terhapus)
        if (!File.Exists(_dbPath))
        {
            Console.WriteLine($"[!] Fatal Error: File database '{_dbPath}' tidak ditemukan!");
            return null;
        }

        try
        {
            string jsonString = File.ReadAllText(_dbPath);
            var users = JsonSerializer.Deserialize<List<UserAccount>>(jsonString);

            // Mencari user yang cocok
            if (users != null)
            {
                foreach (var user in users)
                {
                    if (user.Username == username && user.Password == password)
                        return user; // Login berhasil
                }
            }
        }
        catch (JsonException)
        {
            Console.WriteLine("[!] Fatal Error: Format JSON rusak.");
        }

        return null; // Login gagal
    }
}
