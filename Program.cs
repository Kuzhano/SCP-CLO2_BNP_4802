using DeLFINA_CLI;

namespace DeLFINA_CLI
{ 
    class Program
    {
        static void Main(string[] args)
        {
            AppConfig config = AppConfig.LoadConfiguration();

            String jsonDaftarProposal = "proposals.json";
            String jsonDaftarUsers = "users.json";

            // Inisiasi Repositori Bersama
            var repositoryBersama = new JsonRepository<Proposal>(jsonDaftarProposal);
            IAuthApi authService = new JsonAuthService(jsonDaftarUsers);
            IArchivingServiceAPI<Proposal> archivingApi = new PengarsipDataEkspor<Proposal>();

            // Inisialisasi Modul
            var pendaftaranService = new PendaftaranService(repositoryBersama);
            var reviewService = new ReviewService(repositoryBersama);
            var dashboardService = new DashboardService(repositoryBersama, config);
            var eksporService = new EksporService(repositoryBersama, archivingApi);

            // Inject ke Modul IME
            CliAutomata menuApp = new CliAutomata(authService, pendaftaranService, reviewService, dashboardService, eksporService);

            menuApp.Run();
        }
    } 
}