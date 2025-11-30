public class Program {
    public static void Main(string[] args) {
        Menu menu = new Menu();

        int option;
        do {
            menu.Display();
            option = menu.HandleInput();
        } while (option != -1);
    }
}
