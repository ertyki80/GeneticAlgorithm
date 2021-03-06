using System.ComponentModel;
using GeneticAlgorithm.Helpers;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Implementations.OperatorStrategy
{
    [DisplayName("Performance")]
    public class PerformanceGenerationStrategy : IGenerationStrategy
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceGenerationStrategy"/> class.
        /// </summary>
        public PerformanceGenerationStrategy()
        {
            GenerationsNumber = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceGenerationStrategy"/> class.
        /// </summary>
        /// <param name="generationsNumber">The number of generations to keep in the population</param>
        public PerformanceGenerationStrategy(int generationsNumber)
        {
            GenerationsNumber = generationsNumber;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the number of generations to keep in the population.
        /// <remars>The default is 1.</remars>
        /// </summary>
        public int GenerationsNumber { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Register that a new generation has been created.
        /// </summary>
        /// <param name="population">The population where the new generation has been created.</param>
        public void RegisterNewGeneration(IPopulation population)
        {
            ExceptionHelper.ThrowIfNull("population", population);

            if (population.Generations.Count > GenerationsNumber)
            {
                population.Generations.RemoveAt(0);
            }
        }
        #endregion
    }
}
