using System;

namespace DeLFINA_CLI
{
    public class Proposal
    {
        public string IdProposal { get; set; }
        public string Judul { get; set; }
        public string StatusPenerimaan { get; set; } // PENDING, REVISI, DITOLAK, DITERIMA
        public string LinkPdf { get; set; }
        public string TanggalSubmisi { get; set; }
        public string CatatanReview { get; set; }
        public string Pengaju { get; set; }
        public string TanggalPresentasi { get; set; }
    }
}