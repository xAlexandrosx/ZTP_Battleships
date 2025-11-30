public class RegistrationService {
    private const string PLAYER_FILE = "players.csv";
    private readonly Menu m;

    public RegistrationService(Menu menu) {
        m = menu;
    }

    private List<string> ReadPlayers() {
        try {
            if (!File.Exists(PLAYER_FILE))
                File.Create(PLAYER_FILE).Dispose();

            return File.ReadAllLines(PLAYER_FILE)
                .Select(line => line.Split(',')[0].Trim())
                .ToList();
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return new List<string>();
        }
    }
    
    private void WritePlayer(string name) {
        try {
            using (StreamWriter sw = new StreamWriter(PLAYER_FILE, true)) {
                sw.WriteLine(name);
            }
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
        }
    }

    private bool IsLoggedIn(string playerName) {
        return (m.GetP1() != null && m.GetP1().Name == playerName) ||
               (m.GetP2() != null && m.GetP2().Name == playerName);
    }

    public void LogIn(string playerName) {
        List<string> players = ReadPlayers();

        if (!players.Contains(playerName)) {
            Console.WriteLine("No such account exists.");
            return;
        }

        if (IsLoggedIn(playerName)) {
            Console.WriteLine("Player already logged in.");
            return;
        }

        if (m.GetP1() == null) {
            m.SetP1(new HumanPlayer(playerName));
            Console.WriteLine($"{playerName} logged in as Player 1.");
        }
        else if (m.GetP2() == null) {
            m.SetP2(new HumanPlayer(playerName));
            Console.WriteLine($"{playerName} logged in as Player 2.");
        }
        else {
            Console.WriteLine("Two players are already logged in.");
        }
    }

    public void LogOut(int playerIndex) {
        if (playerIndex == 0 && m.GetP1() != null) {
            Console.WriteLine($"{m.GetP1().Name} logged out.");
            m.SetP1(null);
            return;
        }
        if (playerIndex == 1 && m.GetP2() != null) {
            Console.WriteLine($"{m.GetP2().Name} logged out.");
            m.SetP2(null);
            return;
        }

        Console.WriteLine($"No player logged in at index {playerIndex}.");
    }

    public void SignIn(string playerName) {
        List<string> players = ReadPlayers();

        if (players.Contains(playerName)) {
            Console.WriteLine("Name already taken.");
            return;
        }

        WritePlayer(playerName);
        Console.WriteLine($"Account created for: {playerName}");
    }
}
