using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Modul_Pengarsipan_dan_Ekspor_Data
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<ProposalProduksiKonten> databasePK;
            List<ProposalHLE> databaseHLE;

            try
            {
                string jsonString = File.ReadAllText("daftar_proposal.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                DataWrapper data = JsonSerializer.Deserialize<DataWrapper>(jsonString, options);

                databasePK = data.ProposalProduksiKonten;
                databaseHLE = data.ProposalHLE;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error membaca JSON: " + ex.Message);
                return;
            }

            IArchivingServiceAPI<ProposalProduksiKonten> servicePK = new PengarsipDataEkspor<ProposalProduksiKonten>();
            IArchivingServiceAPI<ProposalHLE> serviceHLE = new PengarsipDataEkspor<ProposalHLE>();

            string roleAwal = "";
            while (true)
            {
                Console.Write("Masukkan Peran/Role Akses Anda (Admin/Dosen): ");
                roleAwal = Console.ReadLine() ?? "";

                if (roleAwal.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                    roleAwal.Equals("Dosen", StringComparison.OrdinalIgnoreCase))
                {
                    break; 
                }

                Console.WriteLine("\n[ERROR] Akses ditolak! Hanya Admin atau Dosen yang diperbolehkan.");
            }

            ArchivingMenu menuUtama = new ArchivingMenu(databasePK, databaseHLE, servicePK, serviceHLE, roleAwal);
            menuUtama.TampilkanMenuUtama();
        }
    }
}