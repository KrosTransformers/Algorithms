using System;
using System.IO;

namespace Recursion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Full folder path: ");
            string path = Console.ReadLine();
            if (!path.EndsWith(@"\"))
            {
                path += @"\";
            }

            PrintDirectory(path);

            Console.ReadKey();
        }

        /// <summary>
        /// Prints directory and its subdirectories and files hierarchically.
        /// </summary>
        /// <param name="path">Path to directory.</param>
        /// <param name="level">Hierarchy level.</param>
        private static void PrintDirectory(string path, int level = 0)
        {
            string indent = new string(' ', level * 3);
                        
            Console.WriteLine($"{indent}{Path.GetFileName(Path.GetDirectoryName(path))}");
            foreach (string directory in Directory.GetDirectories(path))
            {
                PrintDirectory($@"{directory}\", level + 1);
            }
            foreach (string file in Directory.GetFiles(path))
            {
                PrintFileName(file, level + 1);
            }
        }

        /// <summary>
        /// Prints file name hierarchically.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <param name="level">Hierarchy level.</param>
        private static void PrintFileName(string path, int level = 0)
        {
            string indent = new string(' ', level * 3);
            Console.WriteLine($"{indent}{Path.GetFileName(path)}");
        }
    }
}
