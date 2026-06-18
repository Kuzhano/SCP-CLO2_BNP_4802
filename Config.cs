using System;
using System.Collections.Generic;
using System.Text;

namespace Modul_Pemantauan_dan_Dasboard
{
    // RUNTIME CONFIG
    // Membaca pengaturan dari file config.txt saat program dijalankan — tidak perlu mengubah kode jika setting berubah
    class Config
    {
        public string Judul = "Dashboard Proposal";
        public int MaxBaris = 10;

        public static Config Baca()
        {
            var p = new Config();
            if (!File.Exists("config.txt")) return p;

            foreach (var baris in File.ReadAllLines("config.txt"))
            {
                // Format: Kunci=Nilai
                if (!baris.Contains("=")) continue;
                string kunci = baris.Split('=')[0].Trim();
                string nilai = baris.Split('=')[1].Trim();

                if (kunci == "Judul") p.Judul = nilai;
                if (kunci == "MaxBaris") p.MaxBaris = int.Parse(nilai);
            }
            return p;
        }
    }
}
