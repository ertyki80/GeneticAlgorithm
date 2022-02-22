using System.Collections.Generic;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm.Interfaces
{
    public interface IGeneticAlgorithmsForFindMinimum
    {
        public void Initialize(
            double[] limitX,
            double[] limitY,
            int populationMinSize,
            int populationMaxSize,
            float crossoverProbability,
            float mutationProbability);

        public List<GeneticAlgorithmResult> Run();
    }
}