using System;
using System.Collections.Generic;

namespace Modul_Pengarsipan_dan_Ekspor_Data
{
    public class ArchivingMenu
    {
        private List<ProposalProduksiKonten> _daftarPK;
        private List<ProposalHLE> _daftarHLE;
        private IArchivingServiceAPI<ProposalProduksiKonten> _servicePK;
        private IArchivingServiceAPI<ProposalHLE> _serviceHLE;
        private string _currentRole;

        public ArchivingMenu(
            List<ProposalProduksiKonten> dbPK,
            List<ProposalHLE> dbHLE,
            IArchivingServiceAPI<ProposalProduksiKonten> servicePK,
            IArchivingServiceAPI<ProposalHLE> serviceHLE,
            string roleAwal)
        {
            _daftarPK = dbPK;
            _daftarHLE = dbHLE;
            _servicePK = servicePK;
            _serviceHLE = serviceHLE;
            _currentRole = roleAwal;
        }

        public void TampilkanMenuUtama()
        {
            while (true)
            {
                Console.WriteLine("\n=============================================");
                Console.WriteLine($" SYSTEM ARCHIVING PROPOSAL - ROLE: [{_currentRole.ToUpper()}]");
                Console.WriteLine("=============================================");
                Console.WriteLine("1. Tampilkan Semua Proposal Produksi Konten");
                Console.WriteLine("2. Tampilkan Semua Proposal HLE");
                Console.WriteLine("3. Saring & Ekspor Proposal Produksi Konten Selesai");
                Console.WriteLine("4. Saring & Ekspor Proposal HLE Selesai");
                Console.WriteLine("5. Keluar Aplikasi"); 
                Console.Write("Pilih opsi menu (1-5): ");

                string pilihan = Console.ReadLine();

                if (pilihan == "1")
                {
                    Console.WriteLine("\n--- DATA PROPOSAL PRODUKSI KONTEN ---");
                    foreach (var p in _daftarPK)
                    {
                        Console.WriteLine($"[{p.ID_Proposal_PK}] MK: {p.Nama_Mata_Kuliah} | Judul: {p.Judul} | Status: {p.Status_Penerimaan}");
                    }
                }
                else if (pilihan == "2")
                {
                    Console.WriteLine("\n--- DATA PROPOSAL HIGH-LEVEL ENTERPRISE (HLE) ---");
                    foreach (var h in _daftarHLE)
                    {
                        Console.WriteLine($"[{h.ID_Proposal_HLE}] Topik: {h.Topik} | Judul: {h.Judul} | Status: {h.Status_Penerimaan}");
                    }
                }
                else if (pilihan == "3")
                {
                    Console.WriteLine("\n--- PROSES PENGARSIPAN PROPOSAL PRODUKSI KONTEN ---");
                    var dataSelesai = _servicePK.SaringProposalSelesai(_daftarPK);
                    Console.WriteLine($"[Sistem] Menemukan {dataSelesai.Count} proposal berstatus 'Selesai'.");

                    if (dataSelesai.Count > 0)
                    {
                        Console.Write("Masukkan nama file output (tanpa ekstensi): ");
                        string namaFile = Console.ReadLine();
                        Console.Write("Pilih Format Output Runtime (CSV/TXT): ");
                        string formatConfig = Console.ReadLine()?.ToUpper() ?? "CSV";

                        _servicePK.EksporDataBerdasarkanKonfigurasi(dataSelesai, namaFile, formatConfig, _currentRole);
                    }
                }
                else if (pilihan == "4")
                {
                    Console.WriteLine("\n--- PROSES PENGARSIPAN PROPOSAL HLE ---");
                    var dataSelesaiHLE = _serviceHLE.SaringProposalSelesai(_daftarHLE);
                    Console.WriteLine($"[Sistem] Menemukan {dataSelesaiHLE.Count} proposal berstatus 'Selesai'.");

                    if (dataSelesaiHLE.Count > 0)
                    {
                        Console.Write("Masukkan nama file output (tanpa ekstensi): ");
                        string namaFile = Console.ReadLine();
                        Console.Write("Pilih Format Output Runtime (CSV/TXT): ");
                        string formatConfig = Console.ReadLine()?.ToUpper() ?? "CSV";

                        _serviceHLE.EksporDataBerdasarkanKonfigurasi(dataSelesaiHLE, namaFile, formatConfig, _currentRole);
                    }
                }
                else if (pilihan == "5")
                {
                    Console.WriteLine("\nTerima kasih! Program ditutup.");
                    break;
                }
                else
                {
                    Console.WriteLine("\nPilihan menu tidak valid.");
                }

                Console.WriteLine("\nTekan [ENTER] untuk kembali ke menu...");
                Console.ReadLine();
            }
        }
    }
}
