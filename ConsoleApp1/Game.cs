public class Game {
    private Player p1;
    private Player p2;

    public void SetupGame(int size, Player p1, Player p2) {
        Board b1 = new Board(size);
        Board b2 = new Board(size);

        this.p1 = p1;
        this.p2 = p2;

        p1.OwnBoard = b1;
        p1.EnemyBoard = b2;

        p2.OwnBoard = b2;
        p2.EnemyBoard = b1;

        b1.DeployShips(AI.GetBattleshipsRandom(size));
        b2.DeployShips(AI.GetBattleshipsRandom(size));
    }

    public void RunGame() {
        while (true) {
            Console.WriteLine("\n==========================");
            Console.WriteLine($"      {p1.Name}'s turn");
            Console.WriteLine("==========================");
            p1.TakeTurn();

            if (p2.OwnBoard.Ships.Count == 0) {
                Console.WriteLine($"{p1.Name} wins!");
                return;
            }

            Console.WriteLine("\n==========================");
            Console.WriteLine($"      {p2.Name}'s turn");
            Console.WriteLine("==========================");
            p2.TakeTurn();

            if (p1.OwnBoard.Ships.Count == 0) {
                Console.WriteLine($"{p2.Name} wins!");
                return;
            }
        }
    }

}