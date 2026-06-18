using System;
using System.Collections.Generic;
using System.Text;

namespace Modul_Pemantauan_dan_Dasboard
{
    class Controller
    {
        // Mencetak garis pembatas
        public static void Garis() => Console.WriteLine(new string('-', 60));

        // Mencetak satu baris tabel dengan format seragam
        public static void CetakBaris(Proposal p)
        {
            Console.WriteLine($"  {p.Id,-3} | {p.Nama,-10} | {p.Judul,-22} | {p.Status,-10} | {p.TglPresentasi}");
        }

        // Mencari proposal berdasarkan ID
        public static Proposal Cari(List<Proposal> daftar, int id)
        {
            foreach (var p in daftar)
                if (p.Id == id) return p;
            return null;
        }
    }
}
