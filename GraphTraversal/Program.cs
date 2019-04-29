using System;
using System.Collections.Generic;

namespace GraphTraversal
{
    class Program
    {
        /// <summary>
        /// Node cound.
        /// </summary>
        private const int SIZE = 8;

        /// <summary>
        /// Graph with information about edges between nodes.
        /// </summary>
        private static bool[,] _graph = new bool[,]
                                        {
                                            {false, false, false, true, false, false, false, false},
                                            {true, false, false, false, false, false, false, false},
                                            {false, false, false, false, true, false, false, true},
                                            {false, false, false, false, false, true, true, true},
                                            {false, true, false, false, false, false, true, false},
                                            {false, false, false, false, false, false, false, false},
                                            {false, false, false, false, false, false, false, false},
                                            {false, false, false, false, false, false, false, false},
                                        };
        /// <summary>
        /// List of already visited nodes.
        /// </summary>
        private static List<int> _visited = new List<int>();
        /// <summary>
        /// Nodes to visit when using DFS.
        /// </summary>
        private static Stack<int> _toVisitDFS = new Stack<int>();
        /// <summary>
        /// Nodes to visit when using BFS.
        /// </summary>
        private static Queue<int> _toVisitBFS = new Queue<int>();

        static void Main(string[] args)
        {
            Console.Write($"Start point (0 - {SIZE- 1}): ");
            int startNode = int.Parse(Console.ReadLine());

            TraverseBFS(startNode);
            if (_visited.Count == SIZE)
            {
                Console.WriteLine($"A path exists to every node in the graph from node {startNode}.");
            }
            else
            {
                Console.WriteLine($"A path does not exist to every node in the graph from node {startNode}.");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Processes graph node in DFS manner.
        /// </summary>
        /// <param name="node">Graph node to process.</param>
        private static void TraverseDFS(int node)
        {
            if (!_visited.Contains(node))
            {
                _visited.Add(node);
                for (int child  = 0; child < SIZE; child++)
                {
                    if (_graph[node, child] && !_visited.Contains(child))
                    {
                        _toVisitDFS.Push(child);
                    }
                }
            }

            Console.WriteLine($"Visited:  {string.Join(", ", _visited)}");
            Console.WriteLine($"To visit: {string.Join(", ", _toVisitDFS)}");
            Console.WriteLine();

            if (_toVisitDFS.Count > 0)
            {
                int nextNode = _toVisitDFS.Pop();
                TraverseDFS(nextNode);
            }
        }

        /// <summary>
        /// Processes graph node in DFS manner.
        /// </summary>
        /// <param name="node">Graph node to process.</param>
        private static void TraverseBFS(int node)
        {
            if (!_visited.Contains(node))
            {
                _visited.Add(node);
                for (int child = 0; child < SIZE; child++)
                {
                    if (_graph[node, child] && !_visited.Contains(child))
                    {
                        _toVisitBFS.Enqueue(child);
                    }
                }
            }

            Console.WriteLine($"Visited:  {string.Join(", ", _visited)}");
            Console.WriteLine($"To visit: {string.Join(", ", _toVisitBFS)}");
            Console.WriteLine();

            if (_toVisitBFS.Count > 0)
            {
                int nextNode = _toVisitBFS.Dequeue();
                TraverseBFS(nextNode);
            }
        }
    }
}
