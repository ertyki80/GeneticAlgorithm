using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Implementations.FitnessService
{

    [DisplayName("Fitness Based")]
    public class FitnessBasedReinsertion : ReinsertionBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessBasedReinsertion"/> class.
        /// </summary>
        public FitnessBasedReinsertion() : base(true, false)
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
            if (offspring.Count > population.MaxSize)
            {
                return offspring.OrderByDescending(o => o.Fitness).Take(population.MaxSize).ToList();
            }

            return offspring;
        }
        #endregion
    }
}