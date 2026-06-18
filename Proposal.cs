using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Modul_Pemantauan_dan_Dasboard
{
    public class Proposal
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Judul { get; set; }
        public string Status { get; set; }
        public string TglPresentasi { get; set; }
        public string Keterangan { get; set; }
    }
}
