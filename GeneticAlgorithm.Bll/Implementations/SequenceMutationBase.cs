using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Helpers;
using GeneticAlgorithm.Helpers.Randomization;
using GeneticAlgorithm.Implementations.Mutation;
using GeneticAlgorithm.Interfaces;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm.Implementations
{
    /// <summary>
    /// Base class for Mutations on a Sub-Sequence.
    /// </summary>
    public abstract class SequenceMutationBase : MutationBase
    {
        #region Methods
        /// <summary>
        /// Mutate the specified chromosome.
        /// </summary>
        /// <param name="chromosome">The chromosome.</param>
        /// <param name="probability">The probability to mutate each chromosome.</param>
        protected override void PerformMutate(IChromosome chromosome, float probability)
        {
            ValidateLength(chromosome);

            if (RandomizationProvider.Current.GetDouble() <= probability)
            {
                int[] indexes = RandomizationProvider.Current.GetUniqueInts(2, 0, chromosome.Length).OrderBy(i => i).ToArray();
                int firstIndex = indexes[0];
                int secondIndex = indexes[1];
                int sequenceLength = (secondIndex - firstIndex) + 1;

                Gene[] mutatedSequence = MutateOnSequence(chromosome.GetGenes().Skip(firstIndex).Take(sequenceLength)).ToArray();
                
                chromosome.ReplaceGenes(firstIndex, mutatedSequence);
            }
        }

        /// <summary>
        /// Validate length of the chromosome.
        /// </summary>
        /// <param name="chromosome">The chromosome.</param>
        protected virtual void ValidateLength(IChromosome chromosome)
        {
            if (chromosome.Length < 3)
            {
                throw new MutationException(this, "A chromosome should have, at least, 3 genes. {0} has only {1} gene.".With(chromosome.GetType().Name, chromosome.Length));
            }
        }

        /// <summary>
        /// Mutate selected sequence.
        /// </summary>
        /// <returns>The resulted sequence after mutation operation.</returns>
        /// <param name="sequence">The sequence to be mutated.</param>
        protected abstract IEnumerable<T> MutateOnSequence<T>(IEnumerable<T> sequence);
        #endregion
    }
}
