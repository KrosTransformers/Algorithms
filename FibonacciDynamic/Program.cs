using System;

namespace FibonacciDynamic
{
    class Program
    {

        /// <summary>
        /// Cache for storing subproblem results.
        /// </summary>
        private static long[] _cache = new long[200];

        static void Main(string[] args)
        {
            for (int i = 0; i < 200; i++)
            {
                _cache[i] = -1;
            }

            Console.Write("Insert Fibonacci sequence index: ");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(Fibonacci(n));

            Console.ReadKey();
        }

        /// <summary>
        /// Calculates n-th Fibonacci member.
        /// </summary>
        /// <param name="n">Index.</param>
        /// <returns>N-th Fibonacci member.</returns>
        private static long Fibonacci(int n)
        {
            if (n <= 1)
            {
                return 1;
            }
            else
            {
                if (_cache[n] != -1)
                {
                    return _cache[n];
                }
                {
                    long ret = Fibonacci(n - 1) + Fibonacci(n - 2);
                    _cache[n] = ret;

                    return ret;
                }
            }
        }
    }
}
