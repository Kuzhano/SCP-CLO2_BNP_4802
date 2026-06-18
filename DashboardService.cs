using System;
using System.Collections.Generic;

namespace DeLFINA_CLI
{
    public class DashboardService
    {
        private readonly JsonRepository<Proposal> _repository;
        private readonly AppConfig _config;

        public DashboardService(JsonRepository<Proposal> repository, AppConfig config)
        {
            // DbC: Invariant Checks
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), "Repository tidak boleh null.");
            _config = config ?? throw new ArgumentNullException(nameof(config), "Configurasi tidak boleh null.");
        }

        public void TampilkanDashboard()
        {
            Console.Clear();
            Console.WriteLine($"=== {_config.DashboardJudul} ===");
            Console.WriteLine($"Update Terkini: {DateTime.Now:dd MMM yyyy HH:mm}");
            Console.WriteLine();

            List<Proposal> semuaProposal = _repository.GetAllData();

            // --- TABEL 1: SEMUA PROPOSAL ---
            Console.WriteLine("-- SEMUA PROPOSAL --");
            DashboardHelper.CetakGaris();
            DashboardHelper.CetakHeader();
            DashboardHelper.CetakGaris();

            if (semuaProposal.Count == 0)
            {
                Console.WriteLine("| Belum ada data proposal yang diajukan.                                                               |");
            }
            else
            {
                foreach (var p in semuaProposal) DashboardHelper.CetakBaris(p);
            }
            DashboardHelper.CetakGaris();

            // --- TABEL 2: PROGRES (HANYA YANG DITERIMA) ---
            Console.WriteLine("\n-- TABEL PROGRES (Proposal Diterima & Menunggu Presentasi) --");
            DashboardHelper.CetakGaris();
            DashboardHelper.CetakHeader();
            DashboardHelper.CetakGaris();

            int barisTampil = 0;
            foreach (var p in semuaProposal)
            {
                // Filter hanya status DITERIMA sesuai Automata Modul 3
                if (p.StatusPenerimaan == "DITERIMA")
                {
                    if (barisTampil >= _config.DashboardMaxBaris) break; // Limit dari Runtime Config
                    DashboardHelper.CetakBaris(p);
                    barisTampil++;
                }
            }
            DashboardHelper.CetakGaris();

            Console.WriteLine($"\n[Info] Total Proposal Diterima: {barisTampil} (Limit Tampilan: {_config.DashboardMaxBaris} baris)");
            Console.WriteLine("Tekan ENTER untuk kembali ke Menu Utama...");
            Console.ReadLine();
        }
    }
}