using System;

public class Menu {
    private HumanPlayer P1;
    private HumanPlayer P2;
    private int boardSize = 10;

    private readonly RegistrationService rService;

    public Menu() {
        rService = new RegistrationService(this);
    }

    public HumanPlayer GetP1() => P1;
    public HumanPlayer GetP2() => P2;

    public void SetP1(HumanPlayer p) => P1 = p;
    public void SetP2(HumanPlayer p) => P2 = p;

    public int GetBoardSize() => boardSize;

    // MENU DISPLAY
    public void Display() {
        Console.WriteLine("\n===================== MENU =====================");
        Console.WriteLine($"P1: {(P1 == null ? "" : P1.Name)}");
        Console.WriteLine($"P2: {(P2 == null ? "" : P2.Name)}");
        Console.WriteLine($"Board Size: {boardSize}");
        Console.WriteLine();

        bool twoPlayersLogged = (P1 != null && P2 != null);

        Console.WriteLine("1. Start match with 2 players" + (twoPlayersLogged ? "" : " (unavailable)"));
        Console.WriteLine("2. Start match vs AI");
        Console.WriteLine("3. Change board size");
        Console.WriteLine("4. Log out");
        Console.WriteLine("5. Log in");
        Console.WriteLine("6. Register new player");
        Console.WriteLine("7. Exit");
        Console.WriteLine("================================================");
        Console.Write("Choose an option: ");
    }

    public int HandleInput() {
        
        if (!int.TryParse(Console.ReadLine(), out int choice)) {
            Console.WriteLine("Invalid option.");
            return 0;
        }

        switch (choice) {
            case 1: StartMatchPlayers(); break;
            case 2: StartMatchAI(); break;
            case 3: ChangeBoardSize(); break;
            case 4: LogOutMenu(); break;
            case 5: LogInMenu(); break;
            case 6: RegisterMenu(); break;
            case 7:
                Console.WriteLine("Goodbye!");
                return -1;
            default:
                Console.WriteLine("Invalid option."); break;
        }

        return choice;
    }

    private void StartMatchPlayers() {
        
        if (P1 == null || P2 == null) {
            Console.WriteLine("Two players must be logged in!");
            return;
        }

        Console.WriteLine($"Starting match between {P1.Name} and {P2.Name}");

        Game g = new Game();

        Board p1Board = new Board(boardSize);
        Board p2Board = new Board(boardSize);

        P1.SetBoards(p1Board, p2Board);
        P2.SetBoards(p2Board, p1Board);

        p1Board.DeployShips(AI.GetBattleshipsRandom(boardSize));
        p2Board.DeployShips(AI.GetBattleshipsRandom(boardSize));

        g.SetupGame(boardSize, P1, P2);
        g.RunGame();
    }

    private void StartMatchAI() {
        
        Console.WriteLine("Starting match VS AI...");

        Game g = new Game();

        Board p1Board = new Board(boardSize);
        Board p2Board = new Board(boardSize);

        HumanPlayer hp = P1 ?? new HumanPlayer("Guest");

        hp.SetBoards(p1Board, p2Board);
        Player ai = new AI("Computer");
        ai.SetBoards(p2Board, p1Board);

        p1Board.DeployShips(AI.GetBattleshipsRandom(boardSize));
        p2Board.DeployShips(AI.GetBattleshipsRandom(boardSize));

        g.SetupGame(boardSize, hp, ai);
        g.RunGame();
    }

    private void ChangeBoardSize() {
        Console.Write("Enter new board size: ");
        
        if (int.TryParse(Console.ReadLine(), out int newSize)) {
            boardSize = newSize;
            Console.WriteLine("Board size updated.");
        }
        
        else {
            Console.WriteLine("Invalid input.");
        }
    }

    private void LogOutMenu() {
        
        Console.WriteLine("Logout:");
        Console.WriteLine("0 - Log out Player 1");
        Console.WriteLine("1 - Log out Player 2");

        if (int.TryParse(Console.ReadLine(), out int idx)) {
            rService.LogOut(idx);
        }
        else {
            Console.WriteLine("Invalid input.");
        }
    }

    private void LogInMenu() {
        Console.Write("Enter player name to log in: ");
        string name = Console.ReadLine();
        rService.LogIn(name);
    }

    private void RegisterMenu() {
        Console.Write("Enter a new username: ");
        string name = Console.ReadLine();
        rService.SignIn(name);
    }
}
