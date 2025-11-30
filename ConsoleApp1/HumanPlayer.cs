using System;

public class HumanPlayer : Player
{
    public HumanPlayer(string name) : base(name)
    {
    }

    public override void TakeTurn()
    {
        Console.WriteLine($"{Name}, it's your turn!");
        EnemyBoard.DisplayBoard(true);

        int x = ReadInt("Enter X: ");
        int y = ReadInt("Enter Y: ");

        EnemyBoard.RegisterShot(x, y);
    }

    private int ReadInt(string prompt)
    {
        int value;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            Console.Write(prompt);
        }
        return value;
    }

}