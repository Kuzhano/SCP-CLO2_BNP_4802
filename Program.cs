<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
﻿class Program
{
    static void Main(string[] args)
    {
        string dbPath = "users.json";

        // Dependency Injection
        IAuthApi authService = new JsonAuthService(dbPath);
        CliAutomata menuApp = new CliAutomata(authService);

        // Jalankan State Machine
        menuApp.Run();
=======
﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Modul_Pemantauan_dan_Dasboard
{
    class Program
    {
        // Daftar semua proposal
        static List<Proposal> semuaProposal = new List<Proposal>();

        // Tabel progres: hanya proposal yang sudah DISETUJUI
        static List<Proposal> tabelProgres = new List<Proposal>();

        static void Main()
        {
            // Membaca config saat runtime
            var cfg = Config.Baca();

            //Data dummy
            semuaProposal.Add(new Proposal { Id = 1, Nama = "Budi", Judul = "Absensi Digital", Status = "Pending", TglPresentasi = "-", Keterangan = "-" });
            semuaProposal.Add(new Proposal { Id = 2, Nama = "Ani", Judul = "Arsip Online", Status = "Pending", TglPresentasi = "-", Keterangan = "-" });
            semuaProposal.Add(new Proposal { Id = 3, Nama = "Cahyo", Judul = "Pelatihan UMKM", Status = "Pending", TglPresentasi = "-", Keterangan = "-" });
            semuaProposal.Add(new Proposal { Id = 4, Nama = "Dewi", Judul = "Monitoring Sampah", Status = "Pending", TglPresentasi = "-", Keterangan = "-" });

            // Menampilkan Dashboard
            Console.WriteLine();
            Console.WriteLine("  " + cfg.Judul.ToUpper());
            Console.WriteLine("  " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            Controller.Garis(); // Reuse fungsi garis

            Console.WriteLine("\n-- SEMUA PROPOSAL --");
            Console.WriteLine($"  {"ID",-3} | {"Nama",-10} | {"Judul",-22} | {"Status",-10} | Tgl Presentasi");
            Controller.Garis(); // Reuse fungsi garis
            foreach (var p in semuaProposal)
                Controller.CetakBaris(p); // Reuse fungsi cetak baris

            Console.WriteLine("\n-- TABEL PROGRES (Proposal Disetujui) --");
            Console.WriteLine($"  {"ID",-3} | {"Nama",-10} | {"Judul",-22} | {"Status",-10} | Tgl Presentasi");
            Controller.Garis(); // Reuse fungsi garis
            int tampil = 0;
            foreach (var p in tabelProgres)
            {
                if (tampil >= cfg.MaxBaris) break;
                Controller.CetakBaris(p); // Reuse fungsi cetak baris
                tampil++;
            }

            Console.WriteLine();
            Controller.Garis();
            Console.WriteLine("  Proposal disetujui: " + tabelProgres.Count);
            Console.WriteLine("  [config] MaxBaris=" + cfg.MaxBaris + " | Judul=" + cfg.Judul);
            Controller.Garis();


            //Penerimaan proposal
            Setujui(1, "25/06/2026", "Siap dipresentasikan");
            Setujui(2, "02/07/2026", "Anggaran sesuai");
            Tolak(4, "Anggaran terlalu besar");
        }

        // Ubah status jadi Disetujui + masukkan ke tabel progres
        static void Setujui(int id, string tgl, string ket)
        {
            var p = Controller.Cari(semuaProposal, id); // Reuse fungsi Cari
            if (p == null) return;
            p.Status = "Disetujui";
            p.TglPresentasi = tgl;
            p.Keterangan = ket;
            tabelProgres.Add(p); // Otomatis masuk tabel progres
            Console.WriteLine("Disetujui: " + p.Judul);
        }

        // Ubah status jadi Ditolak
        static void Tolak(int id, string alasan)
        {
            var p = Controller.Cari(semuaProposal, id); // Reuse fungsi Cari
            if (p == null) return;
            p.Status = "Ditolak";
            p.Keterangan = alasan;
            Console.WriteLine("Ditolak  : " + p.Judul);
        }
>>>>>>> origin/Modul-Pemantauan-dan-Dasboard
=======
﻿using System;
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
>>>>>>> origin/Modul-Pengarsipan-dan-Ekspor-Data
    }
=======
﻿using SCP_CLO2_BNP_4802;

namespace ModulPendaftaranProposal
=======
﻿using System;

namespace ModulReviewPenilaian
>>>>>>> origin/feature/modul3-review-penilaian
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            // 1. runtime config
            AppConfig config = AppConfig.LoadConfiguration();

            // 2. Inisialisasi controller generics & path dari config
            PendaftaranController<Pendaftaran> controller = new PendaftaranController<Pendaftaran>(config.ProposalFilePath);

            // 3. Menjalankan tampilan menu untuk dosen
            BuatProposal modulDosen = new BuatProposal(controller);
            modulDosen.MenuDosen();
        }
    }
>>>>>>> origin/Modul-pendaftaran-dosen
=======
            ReviewController reviewModule = new ReviewController();
            reviewModule.TampilkanMenuReview();
        }
    }
>>>>>>> origin/feature/modul3-review-penilaian
}