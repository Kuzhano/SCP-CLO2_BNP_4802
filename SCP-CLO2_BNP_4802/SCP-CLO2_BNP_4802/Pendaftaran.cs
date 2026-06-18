using System;
using System.Collections.Generic;
using System.Text;

namespace ModulPendaftaranProposal
{
    public class Pendaftaran
    {
        public string IdProposal { get; set; }
        public string Judul { get; set; }
        public string LinkPdf { get; set; }
        public string TanggalSubmisi { get; set; }
        public string StatusPenerimaan { get; set; }

        public Pendaftaran(string idProposal, string judul, string linkPdf, string tanggalSubmisi, string statusPenerimaan)
        {
            IdProposal = idProposal;
            Judul = judul;
            LinkPdf = linkPdf;
            TanggalSubmisi = tanggalSubmisi;
            StatusPenerimaan = statusPenerimaan;
        }
    }
}