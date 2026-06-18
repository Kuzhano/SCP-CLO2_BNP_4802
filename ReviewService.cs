using System;
using System.Collections.Generic;

namespace DeLFINA_CLI
{
    public class ReviewService
    {
        private readonly JsonRepository<Proposal> _repository;
        private readonly ReviewStateMachine _stateMachine;

        // Dependency Injection untuk Repository
        public ReviewService(JsonRepository<Proposal> repository)
        {
            // DbC: Invariant check
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _stateMachine = new ReviewStateMachine();
        }

        public void TampilkanMenuReview()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== REVIEW & PENILAIAN PROPOSAL (ADMIN) ===");

                // Membaca data menggunakan Generics (Code Reuse)
                List<Proposal> proposals = _repository.GetAllData();

                if (proposals.Count == 0)
                {
                    Console.WriteLine("Tidak ada data proposal untuk direview.");
                    Console.WriteLine("Tekan ENTER untuk kembali.");
                    Console.ReadLine();
                    return; // Kembali ke menu Automata utama
                }

                for (int i = 0; i < proposals.Count; i++)
                {
                    var p = proposals[i];
                    Console.WriteLine($"[{i + 1}] ID: {p.IdProposal} | Judul: {p.Judul} | Status: {p.StatusPenerimaan}");
                }

                Console.WriteLine("[0] Kembali ke Menu Utama");
                Console.Write("\nPilih Nomor Proposal untuk direview: ");

                if (int.TryParse(Console.ReadLine(), out int pilihan))
                {
                    if (pilihan == 0) break; // Kembali ke Modul 1

                    if (pilihan > 0 && pilihan <= proposals.Count)
                    {
                        ProsesReview(proposals[pilihan - 1], proposals);
                    }
                }
            }
        }

        private void ProsesReview(Proposal proposalTarget, List<Proposal> semuaProposal)
        {
            Console.Clear();
            Console.WriteLine("=== DETAIL PROPOSAL ===");
            Console.WriteLine($"ID Proposal   : {proposalTarget.IdProposal}");
            Console.WriteLine($"Judul         : {proposalTarget.Judul}");
            Console.WriteLine($"Tgl Submisi   : {proposalTarget.TanggalSubmisi}");
            Console.WriteLine($"Status Saat Ini: {proposalTarget.StatusPenerimaan}");
            Console.WriteLine($"Catatan Lama  : {proposalTarget.CatatanReview ?? "-"}");

            Console.WriteLine("\n=== AKSI REVIEW ===");
            Console.WriteLine("1. TERIMA");
            Console.WriteLine("2. TOLAK");
            Console.WriteLine("3. MINTA REVISI");
            Console.WriteLine("4. KEMBALIKAN KE PENDING");
            Console.WriteLine("0. Batal");
            Console.Write("Pilih Aksi: ");

            string pilihan = Console.ReadLine();
            AksiReview aksi;

            switch (pilihan)
            {
                case "1": aksi = AksiReview.TERIMA; break;
                case "2": aksi = AksiReview.TOLAK; break;
                case "3": aksi = AksiReview.MINTA_REVISI; break;
                case "4": aksi = AksiReview.KEMBALIKAN_PENDING; break;
                case "0": return;
                default:
                    Console.WriteLine("Pilihan tidak valid!");
                    Console.ReadLine();
                    return;
            }

            try
            {
                StatusProposal currentState = ReviewStateMachine.ParseStatus(proposalTarget.StatusPenerimaan);

                // TK: Eksekusi Automata
                StatusProposal newState = _stateMachine.GetNextState(currentState, aksi);

                Console.Write("\nMasukkan Catatan Evaluasi/Review: ");
                string catatan = Console.ReadLine();

                // Update state di memori
                proposalTarget.StatusPenerimaan = newState.ToString();
                proposalTarget.CatatanReview = catatan;

                // Simpan perubahan ke JSON menggunakan fungsi baru di Generics
                _repository.UpdateAll(semuaProposal);

                Console.WriteLine("\n[SUKSES] Status proposal berhasil diperbarui dan disimpan ke JSON!");
            }
            catch (ArgumentException ex) // Menangkap transisi ilegal dari Automata
            {
                Console.WriteLine($"\n[ERROR DbC] {ex.Message}");
            }

            Console.WriteLine("Tekan ENTER untuk melanjutkan...");
            Console.ReadLine();
        }
    }
}