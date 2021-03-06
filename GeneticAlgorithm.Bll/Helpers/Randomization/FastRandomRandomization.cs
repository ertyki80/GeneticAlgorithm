using System;
using System.Threading;

namespace GeneticAlgorithm.Helpers.Randomization
{
    public class FastRandomRandomization : RandomizationBase
    {
        private static readonly FastRandom _globalRandom = new(DateTime.Now.Millisecond);
        private static readonly object _globalLock = new();

        /// <summary> 
        /// Random number generator 
        /// </summary> 
        private static readonly ThreadLocal<FastRandom> _threadRandom = new(NewRandom);

        /// <summary> 
        /// Creates a new instance of FastRandom. The seed is derived 
        /// from a global (static) instance of Random, rather 
        /// than time. 
        /// </summary> 
        private static FastRandom NewRandom()
        {
            lock (_globalLock)
            {
                return new FastRandom(_globalRandom.Next(0, int.MaxValue));
            }
        }

        /// <summary> 
        /// Returns an instance of Random which can be used freely 
        /// within the current thread. 
        /// </summary> 
        private static FastRandom Instance { get { return _threadRandom.Value; } }

        /// <summary>
        /// Gets an integer value between minimum value (inclusive) and maximum value (exclusive).
        /// </summary>
        /// <returns>The integer.</returns>
        /// <param name="min">Minimum value (inclusive).</param>
        /// <param name="max">Maximum value (exclusive).</param>
        public override int GetInt(int min, int max)
        {
            return Instance.Next(min, max);
        }

        /// <summary>
        /// Gets a float value between 0.0 and 1.0.
        /// </summary>
        /// <returns>
        /// The float value.
        /// </returns>
        public override float GetFloat()
        {
            return (float)Instance.NextDouble();
        }

        /// <summary>
        /// Gets a double value between 0.0 and 1.0.
        /// </summary>
        /// <returns>The double value.</returns>
        public override double GetDouble()
        {
            return Instance.NextDouble();
        }
    }
}
