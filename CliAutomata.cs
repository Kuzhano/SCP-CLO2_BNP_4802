// ==========================================
// AUTOMATA (State Machine)
// ==========================================
public class CliAutomata
{
    public enum AppState { Login, MenuDosen, MenuAdmin, Exit }

    private AppState _currentState;
    private readonly IAuthApi _authApi;
    private UserAccount _loggedInUser;

    // Constructor dengan Injection
    public CliAutomata(IAuthApi authApi)
    {
        // DbC: Invariant check
        _authApi = authApi ?? throw new ArgumentNullException(nameof(authApi), "API Autentikasi gagal dimuat.");
        _currentState = AppState.Login;
    }

    public void Run()
    {
        // Loop utama Automata
        while (_currentState != AppState.Exit)
        {
            try
            {
                switch (_currentState)
                {
                    case AppState.Login:
                        HandleLoginState();
                        break;
                    case AppState.MenuDosen:
                        HandleMenuDosenState();
                        break;
                    case AppState.MenuAdmin:
                        HandleMenuAdminState();
                        break;
                }
            }
            catch (Exception ex)
            {
                // Defensive: Tangkap error apa pun agar program tidak langsung mati
                Console.WriteLine($"[System Error]: {ex.Message}");
                _currentState = AppState.Login; // Fallback state
            }
        }
        Console.WriteLine("Aplikasi dihentikan. Sampai jumpa!");
    }

    private void HandleLoginState()
    {
        Console.Clear();
        Console.WriteLine("=== SISTEM MANAJEMEN PROPOSAL ===");
        Console.Write("Username : ");
        string user = Console.ReadLine();
        Console.Write("Password : ");
        string pass = Console.ReadLine();

        _loggedInUser = _authApi.Login(user, pass);

        if (_loggedInUser != null)
        {
            Console.WriteLine($"Login Sukses! Selamat datang, {_loggedInUser.Username} ({_loggedInUser.Role}).");
            System.Threading.Thread.Sleep(1000);

            // Transisi State Automata berdasarkan Role
            _currentState = _loggedInUser.Role == "Admin" ? AppState.MenuAdmin : AppState.MenuDosen;
        }
        else
        {
            Console.WriteLine("Login gagal! User tidak ditemukan atau password salah.");
            Console.WriteLine("Tekan Enter untuk mencoba lagi...");
            Console.ReadLine();
        }
    }

    private void HandleMenuDosenState()
    {
        Console.Clear();
        Console.WriteLine($"=== MENU DOSEN | User: {_loggedInUser.Username} ===");
        Console.WriteLine("1. Ajukan Proposal Baru");
        Console.WriteLine("2. Lihat Dashboard");
        Console.WriteLine("0. Logout");
        Console.Write("Pilih opsi: ");

        string opsi = Console.ReadLine();
        if (opsi == "1")
        {
            Console.WriteLine("\n[Mock] Memanggil fungsi Modul 2: PendaftaranProposal...");
            Console.ReadLine(); // Pause untuk demo
        }
        else if (opsi == "2")
        {
            Console.WriteLine("\n[Mock] Memanggil fungsi Modul 4: Dashboard...");
            Console.ReadLine(); // Pause untuk demo
        }
        else if (opsi == "0")
        {
            _loggedInUser = null;
            _currentState = AppState.Login; // Transisi Automata ke Login
        }
    }

    private void HandleMenuAdminState()
    {
        Console.Clear();
        Console.WriteLine($"=== MENU ADMIN | User: {_loggedInUser.Username} ===");
        Console.WriteLine("1. Review Proposal");
        Console.WriteLine("2. Lihat Dashboard");
        Console.WriteLine("3. Ekspor Data");
        Console.WriteLine("0. Logout");
        Console.Write("Pilih opsi: ");

        string opsi = Console.ReadLine();
        if (opsi == "1")
        {
            Console.WriteLine("\n[Mock] Memanggil fungsi Modul 3: Review & Penilaian...");
            Console.ReadLine();
        }
        else if (opsi == "3")
        {
            Console.WriteLine("\n[Mock] Memanggil fungsi Modul 5: Ekspor CSV...");
            Console.ReadLine();
        }
        else if (opsi == "0")
        {
            _loggedInUser = null;
            _currentState = AppState.Login; // Transisi Automata ke Login
        }
    }
}