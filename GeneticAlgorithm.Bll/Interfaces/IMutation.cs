namespace GeneticAlgorithm.Interfaces
{

    public interface IMutation : IChromosomeOperator
    {
        void Mutate(IChromosome chromosome, float probability);
    }
}
