using System;

namespace DeLFINA_CLI
{
    public static class DashboardHelper
    {
        // Fungsi Code Reuse untuk menggambar garis
        public static void CetakGaris() => Console.WriteLine(new string('-', 100));

        // Fungsi Code Reuse untuk Header
        public static void CetakHeader()
        {
            Console.WriteLine($"| {"ID",-14} | {"Pengaju",-15} | {"Judul",-28} | {"Status",-12} | {"Tgl Presentasi",-15} |");
        }

        // Fungsi Code Reuse untuk mencetak baris data dengan format string interpolation yang rapi
        public static void CetakBaris(Proposal p)
        {
            // DbC: Null safe check
            if (p == null) return;

            string id = p.IdProposal ?? "-";
            string pengaju = p.Pengaju ?? "-";
            string judul = p.Judul?.Length > 25 ? p.Judul.Substring(0, 25) + "..." : (p.Judul ?? "-");
            string status = p.StatusPenerimaan ?? "-";
            string tgl = string.IsNullOrEmpty(p.TanggalPresentasi) ? "Belum Dijadwalkan" : p.TanggalPresentasi;

            Console.WriteLine($"| {id,-14} | {pengaju,-15} | {judul,-28} | {status,-12} | {tgl,-15} |");
        }
    }
}