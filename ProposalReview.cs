using System;

namespace ModulReviewPenilaian
{
    public class ProposalReview
    {
        public string IdProposal { get; set; }
        public string Judul { get; set; }
        public string LinkPdf { get; set; }
        public string TanggalSubmisi { get; set; }
        public string StatusPenerimaan { get; set; }

        // Tambahan khusus untuk Modul Review
        public string CatatanReview { get; set; }
    }
}