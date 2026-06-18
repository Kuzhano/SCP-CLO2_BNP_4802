using System;

namespace DeLFINA_CLI
{
    public class PendaftaranService
    {
        private readonly JsonRepository<Proposal> _repository;

        public PendaftaranService(JsonRepository<Proposal> repository)
        {
            // DbC: Invariant check untuk memastikan repository tidak null
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), "Repository gagal dimuat di PendaftaranService.");
        }

        // Method ini akan dipanggil oleh Modul 1
        public void BuatProposalBaru(string dosenName)
        {
            Console.Clear();
            Console.WriteLine("=== PENGAJUAN PROPOSAL BARU ===");
            Console.WriteLine($"Pengaju: {dosenName}");

            Console.Write("Masukkan Judul Proposal: ");
            string judul = Console.ReadLine();

            // DbC: Pre-condition validasi input pengguna
            if (string.IsNullOrWhiteSpace(judul))
            {
                Console.WriteLine("\n[!] Error: Judul proposal tidak boleh kosong.");
                Console.WriteLine("Tekan Enter untuk kembali ke menu...");
                Console.ReadLine();
                return; // Langsung potong eksekusi jika input invalid
            }

            Proposal proposalBaru = new Proposal
            {
                IdProposal = $"PROP-{DateTime.Now.Year}-{new Random().Next(100, 999):D3}",
                Pengaju = dosenName,
                Judul = judul,
                LinkPdf = "-", // Belum ada file yang diunggah
                TanggalSubmisi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                StatusPenerimaan = "PENDING",
                CatatanReview = null,
                TanggalPresentasi = null
            };

            try
            {
                _repository.SaveData(proposalBaru);
                Console.WriteLine($"\n[+] Sukses! Proposal '{judul}' berhasil disimpan ke database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[!] Gagal menyimpan data: {ex.Message}");
            }

            Console.WriteLine("Tekan Enter untuk kembali...");
            Console.ReadLine();
        }
    }
}