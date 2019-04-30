using System.Collections.Generic;
using System.Linq;

namespace AssignmentProblem
{

    /// <summary>
    /// Assignement problem solution candidate.
    /// </summary>
    class SolutionCandidate
    {

        #region Attributes

        /// <summary>
        /// Agent/job count.
        /// </summary>
        private int _N;

        /// <summary>
        /// Collection of agents available for children solutions.
        /// </summary>
        private Queue<int> _availableAgents;

        #endregion

        #region Properties

        /// <summary>
        /// Upper bound for candidate quality.
        /// </summary>
        public int UpperBound { get; set; }

        /// <summary>
        /// Agent-job assignements.
        /// </summary>
        public int[] Assignements { get; }

        /// <summary>
        /// Indicator whether solution is complete.
        /// </summary>
        public bool IsComplete { get => Assignements.Length == _N; }

        /// <summary>
        /// Indicator, whether solution candidate is promising (e.g. can lead to best solution).
        /// </summary>
        public bool IsPromising { get; set; }

        #endregion

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="N">Agent/job count.</param>
        public SolutionCandidate(int N):this(N, null) { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="N">Agent/job count.</param>
        /// <param name="assignements">Assigned agents-jobs</param>
        public SolutionCandidate(int N, int[] assignements)
        {
            _N = N;

            UpperBound = 0;
            Assignements = (assignements == null ? new int[0] : assignements);
            _availableAgents = new Queue<int>();
            for (int i = 0; i < _N; i++)
            {
                if (assignements == null || !assignements.Contains(i))
                {
                    _availableAgents.Enqueue(i);
                }
            }

            IsPromising = true;
        }

        /// <summary>
        /// Creates branch for next available agent assignement.
        /// </summary>
        /// <returns>New branch for next agent; null, if no more agents are available.</returns>
        public SolutionCandidate GetBranch()
        {
            if (_availableAgents.Count > 0 && IsPromising)
            {
                int[] branchAssignements = new int[Assignements.Length + 1];
                for (int i = 0; i < Assignements.Length; i++)
                {
                    branchAssignements[i] = Assignements[i];

                }
                branchAssignements[branchAssignements.Length - 1] = _availableAgents.Dequeue();

                return new SolutionCandidate(_N, branchAssignements);
            }
            else
            {
                return null;
            }            
        }
    }
}