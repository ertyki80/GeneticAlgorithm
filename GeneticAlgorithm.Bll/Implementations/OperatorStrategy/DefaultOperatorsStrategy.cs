using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Implementations.OperatorStrategy
{
    public class DefaultOperatorsStrategy : OperatorsStrategyBase
    {
        /// <summary>
        /// Crosses the specified parents.
        /// </summary>
        /// <param name="population">the population from which parents are selected</param>
        /// <param name="crossover">The crossover class.</param>
        /// <param name="crossoverProbability">The crossover probability.</param>
        /// <param name="parents">The parents.</param>
        /// <returns>The result chromosomes.</returns>
        public override IList<IChromosome> Cross(IPopulation population, ICrossover crossover, float crossoverProbability, IList<IChromosome> parents)
        {
            int minSize = population.MinSize;
            List<IChromosome> offspring = new List<IChromosome>(minSize);

            for (int i = 0; i < minSize; i += crossover.ParentsNumber)
            {
                IList<IChromosome> children = SelectParentsAndCross(population, crossover, crossoverProbability, parents, i);
                if (children != null)
                {
                    offspring.AddRange(children);
                }
                
            }

            return offspring;
        }

        /// <summary>
        /// Mutate the specified chromosomes.
        /// </summary>
        /// <param name="mutation">The mutation class.</param>
        /// <param name="mutationProbability">The mutation probability.</param>
        /// <param name="chromosomes">The chromosomes.</param>
        public override void Mutate(IMutation mutation, float mutationProbability, IList<IChromosome> chromosomes)
        {
            for (int i = 0; i < chromosomes.Count; i++)
            {
                mutation.Mutate(chromosomes[i], mutationProbability);
            }
        }
    }
}
