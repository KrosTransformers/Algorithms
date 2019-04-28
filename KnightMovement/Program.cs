using System;

namespace KnightMovement
{
    class Program
    {

        #region Constants

        /// <summary>
        /// Chessboard dimension.
        /// </summary>
        private const int N = 8;
        /// <summary>
        /// Initial coordinates.
        /// </summary>
        private const int X = 0, Y = 0;

        #endregion

        #region Attributes

        /// <summary>
        /// Chessboard.
        /// </summary>
        private static int[,] _chessboard;
        /// <summary>
        /// Current number of moves.
        /// </summary>
        private static int _moves = 0;

        #endregion

        static void Main(string[] args)
        {
            SetChessboard();

            if (FindNextMove(X, Y))
            {
                PrintChessboard();
            }
            else
            {
                Console.WriteLine($"Solution not found for {N}x{N} chessboard.");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Sets chessboard.
        /// </summary>
        private static void SetChessboard()
        {
            _chessboard = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    _chessboard[i, j] = -1;
                }
            }
        }

        /// <summary>
        /// Tries to find next knight move.
        /// </summary>
        /// <param name="x">Previous move X coordinate.</param>
        /// <param name="y">Previous move Y coordinate.</param>
        /// <returns>True, if problem is solved; otherwise false.</returns>
        private static bool FindNextMove(int x, int y)
        {
            _chessboard[x, y] = _moves++;

            if (_moves == N * N)
            {
                return true;
            }

            for (byte direction = 0; direction < 8; direction++)
            {
                (int X, int Y) nextMove = CalculateMove(x, y, direction);

                if (IsOnChessboard(nextMove.X, nextMove.Y) && IsEmpty(nextMove.X, nextMove.Y))
                {
                    if (FindNextMove(nextMove.X, nextMove.Y))
                    {
                        return true;
                    }
                }
            }

            _moves--;
            _chessboard[x, y] = -1;

            return false;
        }

        /// <summary>
        /// Calculates next knight move in of 8 directions.
        /// </summary>
        /// <param name="x">Current knight X coordinate.</param>
        /// <param name="y">Current knight Y coordinate.</param>
        /// <param name="direction">Direction.</param>
        /// <returns>Tuple with suggested move coordinates.</returns>
        private static (int X, int Y) CalculateMove(int x, int y, byte direction)
        {
            switch (direction)
            {
                case 0: return (x + 2, y + 1);
                case 1: return (x + 1, y + 2);
                case 2: return (x - 1, y + 2);
                case 3: return (x - 2, y + 1);
                case 4: return (x - 2, y - 1);
                case 5: return (x - 1, y - 2);
                case 6: return (x + 1, y - 2);
                case 7: return (x + 2, y - 1);
                default: return (x, y);
            }
        }

        /// <summary>
        /// Checks whether field is empty.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns>True, if field has not been used yet; otherwise false.</returns>
        private static bool IsEmpty(int x, int y)
        {
            return (_chessboard[x, y] == -1);
        }

        /// <summary>
        /// Checks whether coordinates belong to chessboard.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns>True, if coordinates are within chessboard bounds; otherwise false.</returns>
        private static bool IsOnChessboard(int x, int y)
        {
            return (x >= 0 && x < N && y >= 0 && y < N);
        }
                
        /// <summary>
        /// Prints chessboard on screen.
        /// </summary>
        private static void PrintChessboard()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write($"{_chessboard[i, j]:00} ");
                }

                Console.WriteLine();
            }
        }
    }
}
