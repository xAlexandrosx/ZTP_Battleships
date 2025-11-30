using System.Text;
public class Board  {
    private readonly int[,] tiles;
    private readonly int size;
    private List<Battleship> ships;

    private const int EMPTY = 0;
    private const int SHIP  = 1;
    private const int HIT   = 2;
    private const int MISS  = 3;

    public int Size => size;
    
    public int GetSize()
    {
        return size;
    }

    public List<Battleship> Ships => ships;

    public Board(int size) {
        this.size = size;
        this.tiles = new int[size, size];
    }

    public void DeployShips(List<Battleship> ships) {
        this.ships = ships;

        foreach (var s in ships) {
            foreach (var pos in s.Tiles) {
                tiles[pos[0], pos[1]] = SHIP;
            }
        }
    }

    public void DisplayBoard(bool isEnemy) {
        StringBuilder sb = new StringBuilder();

        sb.Append("   ");
        for (int j = 0; j < size; j++) sb.Append(j).Append(" ");
        sb.AppendLine();

        for (int i = 0; i < size; i++) {
            sb.Append(i).Append("  ");

            for (int j = 0; j < size; j++) {
                int tile = tiles[i, j];

                if (isEnemy && tile == SHIP) {
                    tile = EMPTY;
                }

                sb.Append(GetSymbol(tile, i, j, isEnemy)).Append(" ");
            }

            sb.AppendLine();
        }

        Console.WriteLine(sb);
    }

    private char GetSymbol(int tile, int x, int y, bool isEnemy) {
        return tile switch {
            EMPTY => '.',
            MISS => 'O',
            SHIP => isEnemy ? '.' : 'S',
            HIT => IsTileFromSunkShip(x, y) ? 'X' : 'x',
            _ => '?'
        };
    }

    public int GetTile(int x, int y) => tiles[x, y];

    public void RegisterShot(int x, int y) {
        if (!InBounds(x, y)) {
            Console.WriteLine("Shot out of bounds!");
            return;
        }

        if (tiles[x, y] == EMPTY) {
            tiles[x, y] = MISS;
            Console.WriteLine("Miss");
            return;
        }

        if (tiles[x, y] == SHIP) {
            tiles[x, y] = HIT;
            Console.WriteLine("Hit");

            Battleship ship = FindShipAt(x, y);

            if (ship == null) {
                Console.WriteLine("ERROR: Ship not found!");
                return;
            }

            if (IsSunk(ship)) {
                Console.WriteLine("Sunk");
                ships.Remove(ship);
            }
            else {
                Console.WriteLine("Not sunk");
            }
        }
    }

    private bool IsSunk(Battleship ship) {
        foreach (var pos in ship.Tiles) {
            if (tiles[pos[0], pos[1]] != HIT) return false;
        }
        return true;
    }

    private Battleship FindShipAt(int x, int y) {
        foreach (var s in ships) {
            foreach (var pos in s.Tiles) {
                if (pos[0] == x && pos[1] == y) return s;
            }
        }
        return null;
    }

    private bool InBounds(int x, int y) => x >= 0 && x < size && y >= 0 && y < size;

    private bool IsTileFromSunkShip(int x, int y) {
        if (ships == null) return false;

        foreach (var ship in ships) {
            bool sunk = true;
            foreach (var t in ship.Tiles) {
                if (tiles[t[0], t[1]] != HIT) {
                    sunk = false;
                    break;
                }
            }

            if (!sunk) continue;

            foreach (var t in ship.Tiles) {
                if (t[0] == x && t[1] == y) return true;
            }
        }

        return false;
    }
}
