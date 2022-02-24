using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GeneticAlgorithm.Helpers.Randomization;

namespace GeneticAlgorithm.Implementations.Mutation
{

    [DisplayName("Partial Shuffle (PSM)")]
    public class PartialShuffleMutation : SequenceMutationBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PartialShuffleMutation"/> class.
        /// </summary>
        public PartialShuffleMutation()
        {
            IsOrdered = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Mutate selected sequence.
        /// </summary>
        /// <returns>The resulted sequence after mutation operation.</returns>
        /// <param name="sequence">The sequence to be mutated.</param>
        protected override IEnumerable<T> MutateOnSequence<T>(IEnumerable<T> sequence)
        {
            // If there is at least two differente genes on source sequence,
            // Then is possible shuffle their in sequence.
            if (sequence.Distinct().Count() > 1)
            {
                IEnumerable<T> result = sequence.Shuffle(RandomizationProvider.Current);
              
                while (sequence.SequenceEqual(result))
                {
                    result = sequence.Shuffle(RandomizationProvider.Current);
                }

                return result; 
            }

            // All genes on sequence are equal, then sequence cannot be shuffled.
            return sequence;
        }
        #endregion
    }
}
