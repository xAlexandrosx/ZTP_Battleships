public class AI : Player {
    private static readonly Random rn = new Random();

    public AI(string name) : base(name) {
    }

    public override void TakeTurn() {
        int size = EnemyBoard.GetSize();
        int x, y;

        while (true) {
            x = rn.Next(size);
            y = rn.Next(size);

            int tile = EnemyBoard.GetTile(x, y);

            if (tile == 0 || tile == 1) break;
        }

        Console.WriteLine($"{Name} shoots at {x}, {y}");
        EnemyBoard.RegisterShot(x, y);
    }

    public static List<Battleship> GetBattleshipsRandom(int size) {
        List<Battleship> ships = new List<Battleship>();
        bool[,] grid = new bool[size, size];

        int[] shipSizes = { 4, 3, 3, 2, 2, 2, 2 };

        foreach (int shipSize in shipSizes) {
            ships.Add(PlaceShip(size, shipSize, grid));
        }

        return ships;
    }

    private static Battleship PlaceShip(int size, int length, bool[,] grid) {
        while (true) {
            bool horizontal = rn.Next(2) == 0;

            int row = rn.Next(size);
            int col = rn.Next(size);

            if (horizontal) {
                if (col + length > size) continue;
            }
            else {
                if (row + length > size) continue;
            }

            bool valid = true;
            for (int i = 0; i < length; i++) {
                int r = horizontal ? row : row + i;
                int c = horizontal ? col + i : col;

                if (grid[r, c]) {
                    valid = false;
                    break;
                }
            }

            if (!valid) continue;

            List<int[]> tiles = new List<int[]>();
            for (int i = 0; i < length; i++) {
                int r = horizontal ? row : row + i;
                int c = horizontal ? col + i : col;
                grid[r, c] = true;
                tiles.Add(new int[] { r, c });
            }

            return new Battleship(tiles);
        }
    }
}
