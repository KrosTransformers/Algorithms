using System;

namespace FibonacciDynamic
{
    class Program
    {

        /// <summary>
        /// Cache for storing subproblem results.
        /// </summary>
        private static double[] _cache = new double[200];

        static void Main(string[] args)
        {
            for (int i = 0; i < 200; i++)
            {
                _cache[i] = -1;
            }

            Console.Write("Insert Fibonacci sequence index (0 - 199): ");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(Fibonacci(n));

            Console.ReadKey();
        }

        /// <summary>
        /// Calculates n-th Fibonacci member.
        /// </summary>
        /// <param name="n">Index.</param>
        /// <returns>N-th Fibonacci member.</returns>
        private static double Fibonacci(int n)
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
                    double ret = Fibonacci(n - 1) + Fibonacci(n - 2);
                    _cache[n] = ret;

                    return ret;
                }
            }
        }
    }
}
