using System;
using System.Collections.Generic;

namespace AssignmentProblem
{
    class Program
    {

        #region Constants

        /// <summary>
        /// Agent/job count.
        /// </summary>
        private const int N = 9;

        #endregion

        #region Attributes

        /// <summary>
        /// Agent/job assignement cost table.
        /// </summary>
        private static int[,] _costTable =
        {
            { 9, 6, 3, 4, 6, 1, 2, 2, 1 },
            { 1, 3, 2, 1, 7, 2, 7, 5, 9 },
            { 7, 9, 8, 3, 9, 1, 4, 2, 7 },
            { 5, 2, 3, 7, 9, 3, 1, 5, 2 },
            { 6, 5, 4, 6, 2, 1, 3, 9, 9 },
            { 4, 9, 9, 2, 1, 9, 4, 7, 1 },
            { 5, 6, 5, 7, 7, 9, 4, 7, 1 },
            { 5, 6, 7, 7, 2, 7, 2, 9, 2 },
            { 5, 2, 3, 9, 8, 8, 3, 7, 6 }
        };

        /// <summary>
        /// Agent names.
        /// </summary>
        private static string[] _agentNames = { "AB", "KS", "MN", "JS", "BK", "MV", "MM", "JC", "MK" };
        /// <summary>
        /// Job names.
        /// </summary>
        private static string[] _jobNames = { "Alfa+", "Olymp", "Omega", "Cenkros4", "NEO BI", "Onix", "iKROS", "Hypo", "VFA" };

        /// <summary>
        /// Max values for individual rows.
        /// </summary>
        private static int[] _maxValues;
        /// <summary>
        /// Current best solution.
        /// </summary>
        private static SolutionCandidate _currentSolution;
        /// <summary>
        /// Found solution candidates.
        /// </summary>
        private static List<SolutionCandidate> _candidates;

        #endregion

        static void Main(string[] args)
        {
            Prepare();
            BranchAndBound();
            PrintAssignements();

            Console.ReadKey();
        }

        /// <summary>
        /// Prepares variables for solving.
        /// </summary>
        private static void Prepare()
        {
            _currentSolution = CreateInitialSolution();

            _candidates = new List<SolutionCandidate>();
            _candidates.Add(new SolutionCandidate(N));

            _maxValues = new int[N];
            for (int i = 0; i < N; i++)
            {
                _maxValues[i] = GetMaxValue(i);
            }
        }

        /// <summary>
        /// Creates initial solution.
        /// </summary>
        /// <returns>Initial solution.</returns>
        private static SolutionCandidate CreateInitialSolution()
        {
            SolutionCandidate initialSolution = new SolutionCandidate(N, new int[N]);

            for (int i = 0; i < N; i++)
            {
                initialSolution.Assignements[i] = i;
                initialSolution.UpperBound += _costTable[i, i];
            }

            return initialSolution;
        }

        /// <summary>
        /// Finds maximum value for agent.
        /// </summary>
        /// <param name="agent">Agent.</param>
        /// <returns>Maximum value for agent.</returns>
        private static int GetMaxValue(int agent)
        {
            int maxValue = int.MinValue;

            for (int i = 0; i < N; i++)
            {
                if (_costTable[agent, i] > maxValue)
                {
                    maxValue = _costTable[agent, i];
                }
            }

            return maxValue;
        }

        /// <summary>
        /// Searches for best solution using branch and bound strategy.
        /// </summary>
        private static void BranchAndBound()
        {
            SolutionCandidate candidate = GetMostPromisingCandidate();
            while (candidate != null)
            {
                SolutionCandidate branch = candidate.GetBranch();
                while (branch != null)
                {
                    CalculateBound(branch);

                    if (branch.UpperBound < _currentSolution.UpperBound)
                    {
                        branch.IsPromising = false;
                    }
                    else if (branch.IsComplete && branch.UpperBound > _currentSolution.UpperBound)
                    {
                        _currentSolution = branch;
                        MarkNotPromisingCandidates();
                    }

                    _candidates.Add(branch);

                    branch = candidate.GetBranch();
                }

                _candidates.Remove(candidate);

                candidate = GetMostPromisingCandidate();
            }
        }

        /// <summary>
        /// Calculates solution candidate branch upper bound.
        /// </summary>
        /// <param name="branch">Branch.</param>
        private static void CalculateBound(SolutionCandidate branch)
        {
            for (int i = 0; i < branch.Assignements.Length; i++)
            {
                branch.UpperBound += _costTable[i, branch.Assignements[i]];
            }
            for (int i = branch.Assignements.Length; i < N; i++)
            {
                branch.UpperBound += _maxValues[i];
            }
        }

        /// <summary>
        /// Marks solution candidates that have no chance of improving current solution.
        /// </summary>
        private static void MarkNotPromisingCandidates()
        {
            foreach (SolutionCandidate candidate in _candidates)
            {
                if (candidate.UpperBound < _currentSolution.UpperBound)
                {
                    candidate.IsPromising = false;
                }
            }
        }

        /// <summary>
        /// Gets most promising solution candidate from currently found solution candidates.
        /// </summary>
        /// <returns>Most promising solution candidate; null, if no such candidate exists.</returns>
        private static SolutionCandidate GetMostPromisingCandidate()
        {
            SolutionCandidate bestCandidate = null;

            foreach (SolutionCandidate candidate in _candidates)
            {
                if (!candidate.IsComplete && candidate.IsPromising && (bestCandidate == null || candidate.UpperBound > bestCandidate.UpperBound))
                {
                    bestCandidate = candidate;
                }
            }

            return bestCandidate;
        }
        
        /// <summary>
        /// Prints final assignment solution.
        /// </summary>
        private static void PrintAssignements()
        {
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine($"{_agentNames[i]}: {_jobNames[_currentSolution.Assignements[i]]}");
            }
            Console.WriteLine();
            Console.WriteLine($"Solution cost: {_currentSolution.UpperBound}");
        }
    }
}
