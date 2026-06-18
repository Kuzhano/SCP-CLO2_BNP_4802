using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeLFINA_CLI
{
    // API (Kontrak Antarmuka Ekspor) & GENERICS
    public interface IArchivingServiceAPI<T> where T : class
    {
        List<T> SaringProposalSelesai(List<T> daftarProposal);
        void EksporDataBerdasarkanKonfigurasi(List<T> dataTerpilih, string namaFileTanpaEkstensi, string formatKonfigurasi, string peranPengguna);
    }

    public class PengarsipDataEkspor<T> : IArchivingServiceAPI<T> where T : Proposal
    {
        public List<T> SaringProposalSelesai(List<T> daftarProposal)
        {
            // DbC: Pre-condition
            if (daftarProposal == null) throw new ArgumentNullException(nameof(daftarProposal), "Data input tidak boleh null!");

            List<T> hasilSaringan = new List<T>();
            foreach (var proposal in daftarProposal)
            {
                if (proposal.StatusPenerimaan != null &&
                    proposal.StatusPenerimaan.Equals("DITERIMA", StringComparison.OrdinalIgnoreCase))
                {
                    hasilSaringan.Add(proposal);
                }
            }
            return hasilSaringan;
        }

        public void EksporDataBerdasarkanKonfigurasi(List<T> dataTerpilih, string namaFileTanpaEkstensi, string formatKonfigurasi, string peranPengguna)
        {
            // DbC: Invariant / Authorization Check
            if (!peranPengguna.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\n[AKSES DITOLAK] Peran '{peranPengguna}' tidak memiliki hak akses untuk mengekspor arsip.");
                return;
            }

            // DbC: Pre-condition
            if (dataTerpilih == null || dataTerpilih.Count == 0)
            {
                Console.WriteLine("\n[PERINGATAN] Tidak ada data proposal berstatus 'DITERIMA' untuk diekspor.");
                return;
            }

            StringBuilder kontenFile = new StringBuilder();
            string namaFileLengkap = "";

            if (formatKonfigurasi.Equals("CSV", StringComparison.OrdinalIgnoreCase))
            {
                namaFileLengkap = namaFileTanpaEkstensi + ".csv";
                kontenFile.AppendLine("ID_Proposal,Pengaju,Judul,Status,Tgl_Submisi,Tgl_Presentasi");
                foreach (var item in dataTerpilih)
                {
                    kontenFile.AppendLine($@"""{item.IdProposal}"",""{item.Pengaju}"",""{item.Judul}"",""{item.StatusPenerimaan}"",""{item.TanggalSubmisi}"",""{item.TanggalPresentasi}""");
                }
            }
            else if (formatKonfigurasi.Equals("TXT", StringComparison.OrdinalIgnoreCase))
            {
                namaFileLengkap = namaFileTanpaEkstensi + ".txt";
                kontenFile.AppendLine("=== REKAPITULASI ARSIP PROPOSAL ===");
                foreach (var item in dataTerpilih)
                {
                    kontenFile.AppendLine($"ID: {item.IdProposal} | Pengaju: {item.Pengaju} | Judul: {item.Judul} | Status: {item.StatusPenerimaan}");
                }
            }
            else
            {
                Console.WriteLine($"\n[ERROR] Format '{formatKonfigurasi}' tidak didukung.");
                return;
            }

            string pathLengkap = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, namaFileLengkap);
            File.WriteAllText(pathLengkap, kontenFile.ToString());

            Console.WriteLine($"\n[SUKSES] Berhasil mengekspor {dataTerpilih.Count} data ke: {namaFileLengkap}");
        }
    }

    // KELAS PENGHUBUNG UI (Controller Modul 5)
    public class EksporService
    {
        private readonly JsonRepository<Proposal> _repository;
        private readonly IArchivingServiceAPI<Proposal> _archivingApi;

        public EksporService(JsonRepository<Proposal> repository, IArchivingServiceAPI<Proposal> archivingApi)
        {
            // DbC: Invariant Checks
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _archivingApi = archivingApi ?? throw new ArgumentNullException(nameof(archivingApi));
        }

        public void TampilkanMenuEkspor(string currentRole)
        {
            Console.Clear();
            Console.WriteLine("=============================================");
            Console.WriteLine(" SYSTEM ARCHIVING & EKSPOR PROPOSAL");
            Console.WriteLine("=============================================");

            // 1. Baca Data dari Repositori Bersama
            var semuaData = _repository.GetAllData();

            // 2. Gunakan API Generics untuk menyaring
            var dataSelesai = _archivingApi.SaringProposalSelesai(semuaData);

            Console.WriteLine($"[Sistem] Menemukan {dataSelesai.Count} proposal berstatus 'DITERIMA'.");

            if (dataSelesai.Count > 0)
            {
                Console.Write("\nMasukkan nama file output (tanpa ekstensi): ");
                string namaFile = Console.ReadLine();

                // Teknik: Runtime Configuration untuk Ekspor
                Console.Write("Pilih Format Output (CSV/TXT): ");
                string formatConfig = Console.ReadLine()?.ToUpper() ?? "CSV";

                // 3. Eksekusi Ekspor
                _archivingApi.EksporDataBerdasarkanKonfigurasi(dataSelesai, namaFile, formatConfig, currentRole);
            }

            Console.WriteLine("\nTekan [ENTER] untuk kembali ke Menu Utama...");
            Console.ReadLine();
        }
    }
}