public class Battleship
{
    private readonly List<int[]> tiles;

    public List<int[]> Tiles => tiles;

    public Battleship(List<int[]> tiles)
    {
        this.tiles = tiles;
    }

}