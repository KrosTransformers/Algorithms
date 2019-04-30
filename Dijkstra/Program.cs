using System;
using System.Collections.Generic;

namespace Dijkstra
{
    class Program
    {

        /// <summary>
        /// Number of graph nodes.
        /// </summary>
        private const int SIZE = 11;

        /// <summary>
        /// Node names for pretty output.
        /// </summary>
        private static string[] _nodeNames = new string[] { "BA", "BR", "KE", "LC", "MT", "NR", "PD", "PP", "RK", "ZA", "ZV" };

        /// <summary>
        /// Distance graph.
        /// </summary>
        private static int[,] _graph = new int[,]
                                        {   // BA   BR   KE   LC   MT   NR   PD   PP   RK   ZA   ZV
                                            {   0,   0,   0,   0,   0,  90,   0,   0,   0, 198,   0},  // BA
                                            {   0,   0, 178,  77,   0,   0,   0,  81,   0,   0,  67},  // BR
                                            {   0, 178,   0, 163,   0,   0,   0, 118,   0,   0,   0},  // KE
                                            {   0,  77, 163,   0,   0, 159,   0,   0,   0,   0,  59},  // LC
                                            {   0,   0,   0,   0,   0,   0,  52,   0,  44,  33,  79},  // MT
                                            {  90,   0,   0, 159,   0,   0,  87,   0,   0,   0, 102},  // NR
                                            {   0,   0,   0,   0,  52,  87,   0,   0,   0,  63,  64},  // PD
                                            {   0,  81, 118,   0,   0,   0,   0,   0,  79,   0,   0},  // PP
                                            {   0,   0,   0,   0,  44,   0,   0,  79,   0,   0,  76},  // RK
                                            { 198,   0,   0,   0,  33,   0,  63,   0,   0,   0,   0},  // ZA
                                            {   0,  67,   0,  59,  79, 102,  64,   0,  76,   0,   0}   // ZV
                                        };

        /// <summary>
        /// Processed nodes.
        /// </summary>
        private static List<int> _SPT;
        /// <summary>
        /// Current minimal distances for individual graph nodes.
        /// </summary>
        private static int[] _minDistances;
        /// <summary>
        /// Paths for minimal distance.
        /// </summary>
        private static List<string>[] _paths;

        static void Main(string[] args)
        {
            Console.Write($"Start node (0 - {SIZE - 1}): ");
            int startNode = int.Parse(Console.ReadLine());

            Prepare(startNode);
            Dijkstra();
            PrintDistances(startNode);

            Console.ReadKey();
        }

        /// <summary>
        /// Prepares initial minimal distances array.
        /// </summary>
        /// <param name="startNode">Start node for algorithm.</param>
        private static void Prepare(int startNode)
        {
            _SPT = new List<int>();

            _minDistances = new int[SIZE];
            _paths = new List<string>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                _minDistances[i] = (i == startNode ? 0 : int.MaxValue);
                _paths[i] = new List<string>();
            }
        }

        /// <summary>
        /// Executes next step of Dijkstra algorithm.
        /// </summary>
        private static void Dijkstra()
        {
            if (_SPT.Count < SIZE)
            {
                int nextNode = FindNodeWithMinimalDistance();

                _SPT.Add(nextNode);

                for (int i = 0; i < SIZE; i++)
                {
                    if (_graph[nextNode, i] > 0 && !_SPT.Contains(i) && _minDistances[nextNode] + _graph[nextNode, i] < _minDistances[i])
                    {
                        _minDistances[i] = _minDistances[nextNode] + _graph[nextNode, i];

                        _paths[i].Clear();
                        _paths[i].AddRange(_paths[nextNode]);
                        _paths[i].Add(_nodeNames[nextNode]);
                    }
                }

                Dijkstra();
            }
        }

        /// <summary>
        /// Prints minimal distances for individual nodes and paths to achieve them.
        /// </summary>
        /// <param name="startNode">Start node.</param>
        private static void PrintDistances(int startNode)
        {
            Console.WriteLine("Distances");
            Console.WriteLine("---------");

            string startNodeName = _nodeNames[startNode];
            for (int i = 0; i < SIZE; i++)
            {
                Console.WriteLine($"{startNodeName} - {_nodeNames[i]}: {_minDistances[i],4} ({string.Join(" - ", _paths[i])} - {_nodeNames[i]})");
            }
        }

        /// <summary>
        /// Finds node with minimal distance, that has not been processed yet.
        /// </summary>
        /// <returns>Index of node; -1 if every node has been processed.</returns>
        private static int FindNodeWithMinimalDistance()
        {
            int min = int.MaxValue;
            int minIndex = -1;

            for (int i = 0; i < SIZE; i++)
            {
                int distance = _minDistances[i];
                if (distance < min && !_SPT.Contains(i))
                {
                    min = distance;
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}
