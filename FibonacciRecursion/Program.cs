using System;

namespace FibonacciRecursion
{
    class Program
    {
        static void Main(string[] args)
        {
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
                return Fibonacci(n - 1) + Fibonacci(n - 2);
            }
        }
    }
}
