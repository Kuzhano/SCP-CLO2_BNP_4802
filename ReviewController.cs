using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ModulReviewPenilaian
{
    public class ReviewController
    {
        private readonly string _filePath = "proposals.json";
        private List<ProposalReview> _proposals;
        private ReviewStateMachine _stateMachine;

        public ReviewController()
        {
            _proposals = new List<ProposalReview>();
            _stateMachine = new ReviewStateMachine();
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                string jsonString = File.ReadAllText(_filePath);
                _proposals = JsonSerializer.Deserialize<List<ProposalReview>>(jsonString) ?? new List<ProposalReview>();
            }
            else
            {
                Console.WriteLine("File proposals.json tidak ditemukan. Belum ada proposal yang diajukan.");
            }
        }

        private void SaveData()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_proposals, options);
            File.WriteAllText(_filePath, jsonString);
        }

        public void TampilkanMenuReview()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== REVIEW & PENILAIAN PROPOSAL (ADMIN) ===");

                if (_proposals.Count == 0)
                {
                    Console.WriteLine("Tidak ada data proposal untuk direview.");
                    Console.WriteLine("Tekan ENTER untuk kembali.");
                    Console.ReadLine();
                    return;
                }

                for (int i = 0; i < _proposals.Count; i++)
                {
                    var p = _proposals[i];
                    Console.WriteLine($"[{i + 1}] ID: {p.IdProposal} | Judul: {p.Judul} | Status: {p.StatusPenerimaan}");
                }

                Console.WriteLine("[0] Kembali ke Menu Utama");
                Console.Write("\nPilih Nomor Proposal untuk direview: ");

                if (int.TryParse(Console.ReadLine(), out int pilihan))
                {
                    if (pilihan == 0) break;

                    if (pilihan > 0 && pilihan <= _proposals.Count)
                    {
                        ReviewProposal(_proposals[pilihan - 1]);
                    }
                }
            }
        }

        private void ReviewProposal(ProposalReview proposal)
        {
            // PRE-CONDITION (Syarat Awal)
            if (proposal == null)
            {
                throw new ArgumentNullException(nameof(proposal), "Data proposal hilang atau tidak valid untuk direview.");
            }

            Console.Clear();
            Console.WriteLine("=== DETAIL PROPOSAL ===");
            Console.WriteLine($"ID Proposal   : {proposal.IdProposal}");
            Console.WriteLine($"Judul         : {proposal.Judul}");
            Console.WriteLine($"Link PDF      : {proposal.LinkPdf}");
            Console.WriteLine($"Tgl Submisi   : {proposal.TanggalSubmisi}");
            Console.WriteLine($"Status Saat Ini: {proposal.StatusPenerimaan}");
            Console.WriteLine($"Catatan Lama  : {proposal.CatatanReview ?? "-"}");

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
                // Ambil status saat ini sebagai Enum
                StatusProposal currentState = ReviewStateMachine.ParseStatus(proposal.StatusPenerimaan);

                // Gunakan Automata untuk mendapatkan status baru
                StatusProposal newState = _stateMachine.GetNextState(currentState, aksi);

                // Masukkan catatan evaluasi
                Console.Write("\nMasukkan Catatan Evaluasi/Review: ");
                string catatan = Console.ReadLine();

                // Update Data
                proposal.StatusPenerimaan = newState.ToString();
                proposal.CatatanReview = catatan;

                SaveData();

                Console.WriteLine("\n[SUKSES] Status proposal berhasil diperbarui!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n[ERROR] {ex.Message}");
            }

            Console.WriteLine("Tekan ENTER untuk melanjutkan...");
            Console.ReadLine();
        }
    }
}