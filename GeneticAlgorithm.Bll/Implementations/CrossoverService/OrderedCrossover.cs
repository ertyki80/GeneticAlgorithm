using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GeneticAlgorithm.Helpers.Randomization;
using GeneticAlgorithm.Implementations.ChromosomeService;
using GeneticAlgorithm.Interfaces;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm.Implementations.CrossoverService
{
    [DisplayName("Ordered (OX1)")]
    public sealed class OrderedCrossover : CrossoverBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedCrossover"/> class.
        /// </summary>
        public OrderedCrossover()
            : base(2, 2)
        {
            IsOrdered = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Performs the cross with specified parents generating the children.
        /// </summary>
        /// <param name="parents">The parents chromosomes.</param>
        /// <returns>The offspring (children) of the parents.</returns>
        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            IChromosome parentOne = parents[0];
            IChromosome parentTwo = parents[1];

            if (parents.AnyHasRepeatedGene())
            {
                throw new CrossoverException(this, "The Ordered Crossover (OX1) can be only used with ordered chromosomes. The specified chromosome has repeated genes.");
            }

            int[] middleSectionIndexes = RandomizationProvider.Current.GetUniqueInts(2, 0, parentOne.Length);
            Array.Sort(middleSectionIndexes);
            int middleSectionBeginIndex = middleSectionIndexes[0];
            int middleSectionEndIndex = middleSectionIndexes[1];
            IChromosome firstChild = CreateChild(parentOne, parentTwo, middleSectionBeginIndex, middleSectionEndIndex);
            IChromosome secondChild = CreateChild(parentTwo, parentOne, middleSectionBeginIndex, middleSectionEndIndex);

            return new List<IChromosome>() { firstChild, secondChild };
        }

        /// <summary>
        /// Creates the child.
        /// </summary>
        /// <returns>The child.</returns>
        /// <param name="firstParent">First parent.</param>
        /// <param name="secondParent">Second parent.</param>
        /// <param name="middleSectionBeginIndex">Middle section begin index.</param>
        /// <param name="middleSectionEndIndex">Middle section end index.</param>
        private static IChromosome CreateChild(IChromosome firstParent, IChromosome secondParent, int middleSectionBeginIndex, int middleSectionEndIndex)
        {
            IEnumerable<Gene> middleSectionGenes = firstParent.GetGenes().Skip(middleSectionBeginIndex).Take((middleSectionEndIndex - middleSectionBeginIndex) + 1);

            using (IEnumerator<Gene> secondParentRemainingGenes = secondParent.GetGenes().Except(middleSectionGenes).GetEnumerator())
            {
                IChromosome child = firstParent.CreateNew();

                for (int i = 0; i < firstParent.Length; i++)
                {
                    Gene firstParentGene = firstParent.GetGene(i);

                    if (i >= middleSectionBeginIndex && i <= middleSectionEndIndex)
                    {
                        child.ReplaceGene(i, firstParentGene);
                    }
                    else
                    {
                        secondParentRemainingGenes.MoveNext();
                        child.ReplaceGene(i, secondParentRemainingGenes.Current);
                    }
                }

                return child;
            }
        }
        #endregion
    }
}
