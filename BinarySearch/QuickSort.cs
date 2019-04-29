using System;

namespace BinarySearch
{

    /// <summary>
    /// Quick Sort algorithm implementation.
    /// </summary>
    class QuickSort
    {

        /// <summary>
        /// Sorts data in ascending order.
        /// </summary>
        /// <param name="data">Data to be sorted.</param>
        /// <returns>Number of operations required to sort the data.</returns>
        public static long Sort(int[] data)
        {
            return Partition(data, 0, data.Length - 1);
        }

        /// <summary>
        /// Partitions data for QuickSort.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="start">Start index of data to be partitioned.</param>
        /// <param name="end">End index of data to be partitioned.</param>
        /// <returns>Number of operations required.</returns>
        public static long Partition(int[] data, int start, int end)
        {
            long operations = 0;

            if (start != end)
            {
                Random random = new Random(Environment.TickCount);
                int pivotIndex = random.Next(start, end + 1);
                int pivot = data[pivotIndex];

                Swap(data, pivotIndex, start);
                operations++;

                int firstOpened = start + 1;
                int lastClosed = start;
                for (int i = start + 1; i <= end; i++)
                {
                    if (data[i] < pivot)
                    {
                        Swap(data, i, firstOpened);
                        operations++;

                        lastClosed = firstOpened;
                        firstOpened++;
                    }
                    operations++;
                }

                Swap(data, start, lastClosed);
                operations++;

                if (lastClosed != start)
                {
                    operations += Partition(data, start, lastClosed - 1);
                }
                if (lastClosed != end)
                {
                    operations += Partition(data, lastClosed + 1, end);
                }
            }

            return operations;
        }

        /// <summary>
        /// Swaps 2 elements in a list.
        /// </summary>
        /// <param name="data">List.</param>
        /// <param name="a">Index of first element.</param>
        /// <param name="b">Index of second element.</param>
        private static void Swap(int[] data, int a, int b)
        {
            int temp = data[a];
            data[a] = data[b];
            data[b] = temp;
        }

    }

}