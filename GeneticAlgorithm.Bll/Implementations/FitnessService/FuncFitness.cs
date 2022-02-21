using System;
using GeneticAlgorithm.Helpers;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Implementations.FitnessService
{
    /// <summary>
    /// An IFitness implementation that defer the fitness evaluation to a Func.
    /// </summary>
    public class FuncFitness : IFitness
    {
        private readonly Func<IChromosome, double> m_func;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GeneticAlgorithm.Implementations.FitnessService.FuncFitness"/> class.
        /// </summary>
        /// <param name="func">The fitness evaluation Func.</param>
        public FuncFitness (Func<IChromosome, double> func)
        {
            ExceptionHelper.ThrowIfNull("func", func);
            m_func = func;
        }

        #region IFitness implementation
        /// <summary>
        /// Evaluate the specified chromosome.
        /// </summary>
        /// <param name="chromosome">Chromosome.</param>
        public double Evaluate (IChromosome chromosome)
        {
            return m_func (chromosome);
        }
        #endregion
    }
}