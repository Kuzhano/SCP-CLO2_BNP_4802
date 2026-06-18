using System;

namespace ModulReviewPenilaian
{
    class Program
    {
        static void Main(string[] args)
        {
            // Pastikan Anda sudah punya file proposals.json dari Modul 2 
            // di directory bin/Debug/netcoreappX.X tempat file exe berjalan.
            ReviewController reviewModule = new ReviewController();
            reviewModule.TampilkanMenuReview();
        }
    }
}