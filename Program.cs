class Program
{
    static void Main(string[] args)
    {
        string dbPath = "users.json";

        // Dependency Injection
        IAuthApi authService = new JsonAuthService(dbPath);
        CliAutomata menuApp = new CliAutomata(authService);

        // Jalankan State Machine
        menuApp.Run();
    }
}