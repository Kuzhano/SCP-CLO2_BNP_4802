using SCP_CLO2_BNP_4802;

namespace ModulPendaftaranProposal
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. runtime config
            AppConfig config = AppConfig.LoadConfiguration();

            // 2. Inisialisasi controller generics & path dari config
            PendaftaranController<Pendaftaran> controller = new PendaftaranController<Pendaftaran>(config.ProposalFilePath);

            // 3. Menjalankan tampilan menu untuk dosen
            BuatProposal modulDosen = new BuatProposal(controller);
            modulDosen.MenuDosen();
        }
    }
}