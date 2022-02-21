using System.Collections.Generic;
using GeneticAlgorithm.Implementations;

namespace GeneticAlgorithm.Interfaces
{
    public interface ISelection
    {
        /// <summary>
        /// Selects the number of chromosomes from the generation specified.
        /// </summary>
        /// <returns>The selected chromosomes.</returns>
        /// <param name="number">The number of chromosomes to select.</param>
        /// <param name="generation">The generation where the selection will be made.</param>
        IList<IChromosome> SelectChromosomes(int number, Generation generation);
    }
}