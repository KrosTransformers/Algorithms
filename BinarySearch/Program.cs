using System;

namespace BinarySearch
{
    class Program
    {
        #region Constants

        /// <summary>
        /// Min number for generating random data.
        /// </summary>
        private const int MIN_NUMBER = -1000;
        /// <summary>
        /// Max number for generating random data.
        /// </summary>
        private const int MAX_NUMBER =  1000;

        #endregion

        static void Main(string[] args)
        {
            Console.Write("Number of elements: ");
            int n = int.Parse(Console.ReadLine());
            Console.Write($"Element to search for ({MIN_NUMBER} - {MAX_NUMBER}): ");
            int x = int.Parse(Console.ReadLine());            

            int[] data = GenerateRandomArray(n);
            Console.WriteLine("Data: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"{data[i],5} ");
                if ((i + 1) % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();

            int index = BinarySearch(data, 0, data.Length - 1, x);
            if (index >= 0)
            {
                Console.WriteLine($"Item was found at index {index}.");
            }
            else
            {
                Console.WriteLine("Item was not found.");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Tries to search for item in specified range of data array.
        /// </summary>
        /// <param name="data">Data array.</param>
        /// <param name="startIndex">Start of searched interval.</param>
        /// <param name="endIndex">End of searched interval.</param>
        /// <param name="x">Searched for item.</param>
        /// <returns>Item index in data array; -1 if array does not contain item.</returns>
        private static int BinarySearch(int[] data, int startIndex, int endIndex, int x)
        {
            int middleIndex = FindMiddleIndex(startIndex, endIndex);
            int middleItem = data[middleIndex];

            if (x == middleItem)
            {
                return middleIndex;
            }
            else if (x > middleItem)
            {
                if (middleIndex == endIndex)
                {
                    return -1;
                }
                else
                {
                    return BinarySearch(data, middleIndex + 1, endIndex, x);
                }
            }
            else
            {
                if (middleIndex == startIndex)
                {
                    return -1;
                }
                else
                {
                    return BinarySearch(data, startIndex, middleIndex - 1, x);
                }
            }
        }

        /// <summary>
        /// Generates sorted random array with specific length.
        /// </summary>
        /// <param name="length">Length of array.</param>
        /// <returns>Sorted array with random numbers.</returns>
        private static int[] GenerateRandomArray(int length)
        {
            int[] ret = new int[length];

            Random r = new Random(Environment.TickCount);
            for (int i = 0; i < length; i++)
            {
                ret[i] = r.Next(MIN_NUMBER, MAX_NUMBER + 1);
            }
            QuickSort.Sort(ret);

            return ret;
        }

        /// <summary>
        /// Find most middle index from interval.
        /// </summary>
        /// <param name="startIndex">Interval start.</param>
        /// <param name="endIndex">Interval end.</param>
        /// <returns></returns>
        private static int FindMiddleIndex(int startIndex, int endIndex)
        {
            return startIndex + (endIndex - startIndex) / 2;
        }
    }
}
