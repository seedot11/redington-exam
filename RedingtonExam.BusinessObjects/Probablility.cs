using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedingtonExam.BusinessObjects
{
    /// <summary>
    /// Used for probability calculations.
    /// </summary>
    public static class Probablility
    {
        private const string ArgumentOutOfRangeErrorMessage = "Argument must be a valid probabilities (between 0 and 1 inclusive).";

        /// <summary>
        /// Calculates the probability that two events with seperate probabilities will happen.
        /// </summary>
        /// <param name="p1">The probability of the first event.</param>
        /// <param name="p2">The probability of the second event.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws an exception if the number is not between 0 and 1.</exception>
        /// <returns>A double percision probability of the two events happening.</returns>
        public static double CombinedWith(double p1, double p2)
        {
            if (!IsValidProbability(p1) || !IsValidProbability(p2))
                throw new ArgumentOutOfRangeException(nameof(ArgumentOutOfRangeException.ParamName), ArgumentOutOfRangeErrorMessage);
            return p1 * p2;
        }

        /// <summary>
        /// Calculates the probability that either of two events with seperate probabilities will happen.
        /// </summary>
        /// <param name="p1">The probability of the first event.</param>
        /// <param name="p2">The probability of the second event.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws an exception if the number is not between 0 and 1.</exception>
        /// <returns>A double percision probability of either of the two events happening.</returns>
        public static double EitherOr(double p1, double p2)
        {
            if (!IsValidProbability(p1) || !IsValidProbability(p2))
                throw new ArgumentOutOfRangeException(nameof(ArgumentOutOfRangeException.ParamName), ArgumentOutOfRangeErrorMessage);
            return p1 + p2 - p1 * p2;
        }

        /// <summary>
        /// Checks whether a probability is a valid probability (between 0 and 1).
        /// </summary>
        /// <param name="p">The probability to check if valid.</param>
        /// <returns>True if the probability is valid, false otherwise.</returns>
        private static bool IsValidProbability(double p)
        {
            return (p >= 0 && p <= 1);
        }
    }
}
