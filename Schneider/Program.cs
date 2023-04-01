namespace schneider
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var game = new GameCore(10, 10, 5);
            game.Start();

            var exitGame = false;

            while (!game.GameComplete && !exitGame)
            {
                Console.WriteLine($"Current position: {game.Position} Score:{game.Score} Lives:{game.Lives}");

                var direction = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (direction)
                {
                    case 'u':
                        game.Up();
                        break;

                    case 'd':
                        game.Down();
                        break;

                    case 'l':
                        game.Left();
                        break;

                    case 'r':
                        game.Right();
                        break;

                    case 'h':
                        Console.WriteLine(game);
                        break;

                    case 'x':
                        exitGame = true;
                        break;
                }

            }

            if (!exitGame)
            {
                Console.WriteLine($"Player hit bomb at {game.Position}");
            }

            Console.WriteLine(game);
        }
    }
}
