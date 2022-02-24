using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Implementations.FitnessService
{

    [DisplayName("Elitist")]
    public class ElitistReinsertion : ReinsertionBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ElitistReinsertion"/> class.
        /// </summary>
        public ElitistReinsertion() : base(false, true)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Selects the chromosomes which will be reinserted.
        /// </summary>
        /// <returns>The chromosomes to be reinserted in next generation..</returns>
        /// <param name="population">The population.</param>
        /// <param name="offspring">The offspring.</param>
        /// <param name="parents">The parents.</param>
        protected override IList<IChromosome> PerformSelectChromosomes(IPopulation population, IList<IChromosome> offspring, IList<IChromosome> parents)
        {
            int diff = population.MinSize - offspring.Count;

            if (diff > 0)
            {
                List<IChromosome> bestParents = parents.OrderByDescending(p => p.Fitness).Take(diff).ToList();

                for (int i = 0; i < bestParents.Count; i++)
                {
                    offspring.Add(bestParents[i]);
                }
            }

            return offspring;
        }
        #endregion
    }
}