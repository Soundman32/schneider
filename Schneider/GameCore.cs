namespace schneider
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    using Ardalis.GuardClauses;

    /// <summary>
    /// Game Core.
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class GameCore
    {
        private string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Initialises a new instance of the <see cref="GameCore"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="bombCount">The bombs.</param>
        public GameCore(int width, int height, int bombCount)
        {
            Guard.Against.NegativeOrZero(height);
            Guard.Against.OutOfRange(height, nameof(height), 0, letters.Length);
            Guard.Against.NegativeOrZero(width);

            Width = width;
            Height = height;
            BombCount = bombCount;
        }

        /// <summary>
        /// Gets the board.
        /// </summary>
        public List<char[]> Board { get; private set; } = null!;

        /// <summary>
        /// Gets the bomb counts.
        /// </summary>
        public int BombCount { get; }

        /// <summary>
        /// Gets a value indicating whether game is complete.
        /// </summary>
        public bool GameComplete => !PlayerSafe || PlayerY < Height - 1 || Lives == 0;

        /// <summary>
        /// Gets the height of the board.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the lives remaining.
        /// </summary>
        public int Lives { get; private set; } = 3;

        /// <summary>
        /// Gets a value indicating whether player is safe.
        /// </summary>
        public bool PlayerSafe => Board[PlayerY][PlayerX] == '.' || Board[PlayerY][PlayerX] == ' ' || Board[PlayerY][PlayerX] == 'O';

        /// <summary>
        /// Gets the player x position.
        /// </summary>
        public int PlayerX { get; private set; }

        /// <summary>
        /// Gets the player y position.
        /// </summary>
        public int PlayerY { get; private set; }

        /// <summary>
        /// Gets the position as a string.
        /// </summary>
        public string Position => $"{letters[PlayerY]}{PlayerX}";

        /// <summary>
        /// Gets the score.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Gets the width of the board.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Move player downs.
        /// </summary>
        /// <returns><c>true</c> if player is safe, <c>false</c> otherwise.</returns>
        public bool Down()
        {
            if (PlayerY < Height - 1)
            {
                SetLastVisited();
                PlayerY++;
                UpdateStatus();
            }

            return PlayerSafe;
        }

        public char[] GetRow(int row)
            => Board[row];

        /// <summary>
        /// Move player Left.
        /// </summary>
        /// <returns><c>true</c> if player is safe, <c>false</c> otherwise.</returns>
        public bool Left()
        {
            if (PlayerX > 0)
            {
                SetLastVisited();
                PlayerX--;
                UpdateStatus();
            }

            return PlayerSafe;
        }

        /// <summary>
        /// Move player right.
        /// </summary>
        /// <returns><c>true</c> if player is safe, <c>false</c> otherwise.</returns>
        public bool Right()
        {
            if (PlayerX < Width - 1)
            {
                SetLastVisited();
                PlayerX++;
                UpdateStatus();
            }

            return PlayerSafe;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Start()
        {
            InitialiseBoard();
            SetInitialPlayerPosition();
            PopulateBombs();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            ForEach((row, col, value) =>
            {
                sb.Append(value);
                if (col == 0 && row != 0)
                {
                    sb.AppendLine();
                }
            });

            return sb.ToString();
        }

        /// <summary>
        /// Move player up.
        /// </summary>
        /// <returns><c>true</c> if player is safe, <c>false</c> otherwise.</returns>
        public bool Up()
        {
            if (PlayerY > 0)
            {
                SetLastVisited();
                PlayerY--;
                UpdateStatus();
            }

            return PlayerSafe;
        }

        private void ForEach(Action<int, int, char> action)
        {
            for (var row = 0; row < Height; row++)
            {
                for (var col = 0; col < Width; col++)
                {
                    action(row, col, Board[row][col]);
                }
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        private void InitialiseBoard()
        {
            Board = new List<char[]>();
            for (int row = 0; row < Height; row++)
            {
                Board.Add(new char[Width]);
            }
            ForEach((row, col, _) => Board[row][col] = ' ');
        }

        private void PopulateBombs()
        {
            var random = new Random();

            var bombsRemaining = BombCount;
            while (bombsRemaining > 0)
            {
                var bx = random.Next(Width);
                var by = random.Next(Height);

                if (Board[bx][by] == ' ')
                {
                    Board[bx][by] = 'B';
                    bombsRemaining--;
                }
            }
        }

        private void SetInitialPlayerPosition()
        {
            PlayerX = Width / 2;
            PlayerY = Height / 2;
            Board[PlayerY][PlayerX] = 'O';
        }

        private void SetLastVisited()
        {
            Board[PlayerY][PlayerX] = '.';
        }

        private void UpdateStatus()
        {
            Score++;

            if (!PlayerSafe)
            {
                Board[PlayerY][PlayerX] = '*';
            }
            else
            {
                Board[PlayerY][PlayerX] = 'O';
            }
        }
    }
}
