using System.ComponentModel;
using GeneticAlgorithm.Helpers.Randomization;
using GeneticAlgorithm.Implementations.ChromosomeService;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Implementations.Mutation
{
    [DisplayName("Flip Bit")]
    public class FlipBitMutation : MutationBase
    {
        #region Fields
        private readonly IRandomization m_rnd;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FlipBitMutation"/> class.
        /// </summary>
        public FlipBitMutation ()
        {
            m_rnd = RandomizationProvider.Current;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Mutate the specified chromosome.
        /// </summary>
        /// <param name="chromosome">The chromosome.</param>
        /// <param name="probability">The probability to mutate each chromosome.</param>
        protected override void PerformMutate (IChromosome chromosome, float probability)
        {
            IBinaryChromosome binaryChromosome = chromosome as IBinaryChromosome;

            if (binaryChromosome == null) 
            {
                throw new MutationException (this, "Needs a binary chromosome that implements IBinaryChromosome.");    
            }

            if (m_rnd.GetDouble() <= probability)
            {
                int index = m_rnd.GetInt(0, chromosome.Length);
                binaryChromosome.FlipGene (index);
            }
        }
        #endregion
    }
}

