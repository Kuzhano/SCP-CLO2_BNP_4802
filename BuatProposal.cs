using SCP_CLO2_BNP_4802;
using System;
using System.Collections.Generic;

namespace ModulPendaftaranProposal
{
    public class BuatProposal
    {
        private PendaftaranController<Pendaftaran> _controller;

        public BuatProposal(PendaftaranController<Pendaftaran> controller)
        {
            _controller = controller;
        }
        public void MenuDosen()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== DASHBOARD DOSEN - MODUL PENDAFTARAN PROPOSAL ===");
                Console.WriteLine("1. Buat Pengajuan Proposal Baru");
                Console.WriteLine("2. Lihat Daftar/Draf Proposal Saya");
                Console.WriteLine("3. Keluar");
                Console.Write("Pilih menu (1-3): ");

                string pilihan = Console.ReadLine();
                switch (pilihan)
                {
                    case "1":
                        FormInputProposal();
                        break;
                    case "2":
                        TampilkanProposalDosen();
                        break;
                    case "3":
                        running = false;
                        Console.WriteLine("Keluar dari modul proposal...");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid! Tekan Enter untuk mencoba lagi.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void FormInputProposal()
        {
            Console.Clear();
            Console.WriteLine("=== FORM PENGAJUAN PROPOSAL BARU ===");

            string idProposal = DateTime.Now.ToString("yyyymmddHHmmss");
            Console.WriteLine($"ID Proposal (Otomatis): {idProposal}");

            Console.Write("Masukkan Judul Proposal: ");
            string judul = Console.ReadLine();

            Console.Write("Masukkan Link PDF: ");
            string linkPdf = Console.ReadLine();

            string tanggalSubmisi = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            string statusPenerimaan = "Pending";

            Pendaftaran proposalBaru = new Pendaftaran(idProposal, judul, linkPdf, tanggalSubmisi, statusPenerimaan);

            _controller.SaveData(proposalBaru);

            Console.WriteLine("Tekan Enter untuk kembali ke menu...");
            Console.ReadLine();
        }

        private void TampilkanProposalDosen()
        {
            Console.Clear();
            Console.WriteLine("===DAFTAR PROPOSAL===");

            List<Pendaftaran> daftarProposal = _controller.GetAllData();

            if (daftarProposal.Count == 0)
            {
                Console.WriteLine("Belum ada proposal yang diajukan.");
            }
            else
            {
                int no = 1;
                foreach (var prop in daftarProposal)
                {
                    Console.WriteLine($"\nNo. {no++}");
                    Console.WriteLine($"ID Proposal       : {prop.IdProposal}");
                    Console.WriteLine($"Judul             : {prop.Judul}");
                    Console.WriteLine($"Link PDF          : {prop.LinkPdf}");
                    Console.WriteLine($"Tanggal Submisi   : {prop.TanggalSubmisi}");
                    Console.WriteLine($"Status Penerimaan : {prop.StatusPenerimaan}");
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.WriteLine("Tekan Enter untuk kembali ke menu...");
            Console.ReadLine();
        }
    }
}