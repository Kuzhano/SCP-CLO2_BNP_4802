using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Modul_Pengarsipan_dan_Ekspor_Data
{
    public interface IArchivingServiceAPI<T> where T : ProposalDasar
    {
        List<T> SaringProposalSelesai(List<T> daftarProposal);
        void EksporDataBerdasarkanKonfigurasi(List<T> dataTerpilih, string namaFileTanpaEkstensi, string formatKonfigurasi, string peranPengguna);
    }

    public class PengarsipDataEkspor<T> : IArchivingServiceAPI<T> where T : ProposalDasar
    {
        public List<T> SaringProposalSelesai(List<T> daftarProposal)
        {
            if (daftarProposal == null) throw new ArgumentNullException(nameof(daftarProposal), "Data input tidak boleh null!");

            List<T> hasilSaringan = new List<T>();
            foreach (var proposal in daftarProposal)
            {
                if (proposal.Status_Penerimaan != null &&
                    proposal.Status_Penerimaan.Equals("Selesai", StringComparison.OrdinalIgnoreCase))
                {
                    hasilSaringan.Add(proposal);
                }
            }
            return hasilSaringan;
        }

        public void EksporDataBerdasarkanKonfigurasi(List<T> dataTerpilih, string namaFileTanpaEkstensi, string formatKonfigurasi, string peranPengguna)
        {
            if (!peranPengguna.Equals("Admin", StringComparison.OrdinalIgnoreCase) &&
                !peranPengguna.Equals("Dosen", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\n[AKSES DITOLAK] Peran '{peranPengguna}' tidak memiliki hak akses untuk arsip.");
                return;
            }

            if (dataTerpilih == null || dataTerpilih.Count == 0)
            {
                Console.WriteLine("\n[PERINGATAN] Tidak ada data proposal berstatus 'Selesai' untuk diekspor.");
                return;
            }

            StringBuilder kontenFile = new StringBuilder();
            string namaFileLengkap = "";

            if (formatKonfigurasi.Equals("CSV", StringComparison.OrdinalIgnoreCase))
            {
                namaFileLengkap = namaFileTanpaEkstensi + ".csv";
                kontenFile.AppendLine("Judul,Status_Penerimaan,File_Proposal,Waktu_Submit");
                foreach (var item in dataTerpilih)
                {
                    kontenFile.AppendLine($@"""{item.Judul}"",""{item.Status_Penerimaan}"",""{item.File_Proposal}"",""{item.Waktu_Submit}""");
                }
            }
            else if (formatKonfigurasi.Equals("TXT", StringComparison.OrdinalIgnoreCase))
            {
                namaFileLengkap = namaFileTanpaEkstensi + ".txt";
                kontenFile.AppendLine("=== REKAPITULASI ARSIP PROPOSAL ===");
                foreach (var item in dataTerpilih)
                {
                    kontenFile.AppendLine($"Judul: {item.Judul} | Status: {item.Status_Penerimaan} | Berkas: {item.File_Proposal} | Submit: {item.Waktu_Submit}");
                }
            }
            else
            {
                Console.WriteLine($"\n[ERROR] Format konfigurasi '{formatKonfigurasi}' tidak didukung.");
                return;
            }

            string pathLengkap = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, namaFileLengkap);
            File.WriteAllText(pathLengkap, kontenFile.ToString());

            Console.WriteLine($"\n[SUKSES] Berhasil mengekspor {dataTerpilih.Count} data ke: {namaFileLengkap}");
            Console.WriteLine($"Lokasi berkas: {pathLengkap}");
        }
    }
}
