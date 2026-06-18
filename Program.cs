using System;

namespace ModulReviewPenilaian
{
    class Program
    {
        static void Main(string[] args)
        {
            ReviewController reviewModule = new ReviewController();
            reviewModule.TampilkanMenuReview();
        }
    }
}