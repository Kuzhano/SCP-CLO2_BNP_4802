using System;
using System.Collections.Generic;

namespace Modul_Pengarsipan_dan_Ekspor_Data
{
    public class ProposalDasar
    {
        public string Judul { get; set; }
        public string Status_Penerimaan { get; set; }
        public string File_Proposal { get; set; }
        public string Waktu_Submit { get; set; }
        public string NID_Dosen_Koordinator { get; set; }

        public ProposalDasar() { }

        public ProposalDasar(string judul, string status, string fileProposal, string waktuSubmit, string nidDosen)
        {
            Judul = judul;
            Status_Penerimaan = status;
            File_Proposal = fileProposal;
            Waktu_Submit = waktuSubmit;
            NID_Dosen_Koordinator = nidDosen;
        }
    }

    public class ProposalProduksiKonten : ProposalDasar
    {
        public string ID_Proposal_PK { get; set; }
        public string Nama_Mata_Kuliah { get; set; }
        public string Kode_Mata_Kuliah { get; set; }
        public string Program_Studi { get; set; }
        public string Fakultas { get; set; }
        public ProposalProduksiKonten() : base() { }

        public ProposalProduksiKonten(
            string idPK, string judul, string status, string fileProposal, string waktuSubmit, string nidDosen,
            string namaMK, string kodeMK, string prodi, string fakultas)
            : base(judul, status, fileProposal, waktuSubmit, nidDosen)
        {
            ID_Proposal_PK = idPK;
            Nama_Mata_Kuliah = namaMK;
            Kode_Mata_Kuliah = kodeMK;
            Program_Studi = prodi;
            Fakultas = fakultas;
        }
    }

    public class ProposalHLE : ProposalDasar
    {
        public string ID_Proposal_HLE { get; set; }
        public string Topik { get; set; }
        public int Kuota { get; set; }
        public string Tanggal_Mulai { get; set; }
        public string Tanggal_Tutup { get; set; }

        public ProposalHLE() : base() { }

        public ProposalHLE(
            string idHLE, string judul, string status, string fileProposal, string waktuSubmit, string nidDosen,
            string topik, int kuota, string tglMulai, string tglTutup)
            : base(judul, status, fileProposal, waktuSubmit, nidDosen)
        {
            ID_Proposal_HLE = idHLE;
            Topik = topik;
            Kuota = kuota;
            Tanggal_Mulai = tglMulai;
            Tanggal_Tutup = tglTutup;
        }
    }

    public class DataWrapper
    {
        public List<ProposalProduksiKonten> ProposalProduksiKonten { get; set; }
        public List<ProposalHLE> ProposalHLE { get; set; }
    }
}
