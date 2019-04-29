using System;

namespace Knapsack
{
    class Program
    {

        #region Constants

        /// <summary>
        /// Item count.
        /// </summary>
        private const int N = 4;
        /// <summary>
        /// Knapsack capacity.
        /// </summary>
        private const int C = 5;

        #endregion

        #region Attributes

        /// <summary>
        /// Item names.
        /// </summary>
        private static string[] _names = { "Algoritmy", "Architektúra IS", "Pokročilé DBS", "Kryptografia" };
        /// <summary>
        /// Item values.
        /// </summary>
        private static int[] _values = { 100, 20, 60, 40};
        /// <summary>
        /// Item weights.
        /// </summary>
        private static int[] _weights = { 3, 2, 4, 1 };

        /// <summary>
        /// Cache.
        /// </summary>
        private static int[,] _cache = new int[N + 1, C + 1];
        /// <summary>
        /// Inclusion matrix.
        /// </summary>
        private static bool[,] _inclusion = new bool[N + 1, C + 1];

        #endregion

        static void Main(string[] args)
        {
            InitializeCache();
            FillCache();
            bool[] includedItems = GetIncludedItems();
            PrintItems(includedItems);

            Console.ReadKey();
        }

        /// <summary>
        /// Initializes cache.
        /// </summary>
        private static void InitializeCache()
        {
            _cache = new int[N + 1, C + 1];
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= C; j++)
                {
                    _cache[i, j] = 0;
                    _inclusion[i, j] = false;
                }
            }
        }

        /// <summary>
        /// Fills cache (bottom-up).
        /// </summary>
        private static void FillCache()
        {
            for (int i = 1; i <= N; i++)
            {
                for (int capacity = 1; capacity <= C; capacity++)
                {
                    int exclude = _cache[i - 1, capacity];
                    int include = (_weights[i - 1] <= capacity ? _values[i - 1] + _cache[i - 1, capacity - _weights[i - 1]] : 0);

                    if (include > exclude)
                    {
                        _inclusion[i, capacity] = true;
                        _cache[i, capacity] = include;
                    }
                    else
                    {
                        _cache[i, capacity] = exclude;
                    }
                }
            }
        }

        /// <summary>
        /// Creates solution from cache.
        /// </summary>
        /// <returns>Array of items inclusion in knapsack.</returns>
        private static bool[] GetIncludedItems()
        {
            bool[] includedItems = new bool[N];
            int remainingCapacity = C;

            for (int i = N; i > 0; i--)
            {
                includedItems[i - 1] = (_inclusion[i, remainingCapacity]);
                remainingCapacity -= (_inclusion[i, remainingCapacity] ? _weights[i - 1] : 0);
            }

            return includedItems;
        }

        /// <summary>
        /// Prints solution.
        /// </summary>
        /// <param name="items">Items included in knapsack.</param>
        private static void PrintItems(bool[] items)
        {
            int usedCapacity = 0;
            int totalValue = 0;

            for (int i = 0; i < N; i++)
            {
                if (items[i])
                {
                    usedCapacity += _weights[i];
                    totalValue += _values[i];
                    Console.WriteLine(_names[i]);
                }
            }
            Console.WriteLine($"Total value: {totalValue}");
            Console.WriteLine($"Used capacity: {usedCapacity}");
        }
    }
}
