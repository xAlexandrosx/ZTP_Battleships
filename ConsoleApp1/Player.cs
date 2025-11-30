public abstract class Player {
    protected readonly string name;
    protected Board ownBoard;
    protected Board enemyBoard;

    public string Name => name;
    public Board OwnBoard { get => ownBoard; set => ownBoard = value; }
    public Board EnemyBoard { get => enemyBoard; set => enemyBoard = value; }

    protected Player(string name) {
        this.name = name;
    }

    public abstract void TakeTurn();

    public void SetBoards(Board p1Board, Board p2Board) {
        ownBoard = p1Board;
        enemyBoard = p2Board;
    }

}