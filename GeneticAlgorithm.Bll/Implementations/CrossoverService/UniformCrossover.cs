using System.Collections.Generic;
using System.ComponentModel;
using GeneticAlgorithm.Helpers.Randomization;
using GeneticAlgorithm.Implementations.ChromosomeService;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Implementations.CrossoverService
{

    [DisplayName("Uniform")]
    public class UniformCrossover : CrossoverBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UniformCrossover"/> class.
        /// </summary>
        /// <param name="mixProbability">The mix probability. he default mix probability is 0.5.</param>
        public UniformCrossover(float mixProbability)
            : base(2, 2)
        {
            MixProbability = mixProbability;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniformCrossover"/> class.
        /// <remarks>
        /// The default mix probability is 0.5.
        /// </remarks>
        /// </summary>
        public UniformCrossover() : this(0.5f)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the mix probability.
        /// </summary>
        /// <value>The mix probability.</value>
        public float MixProbability { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Performs the cross with specified parents generating the children.
        /// </summary>
        /// <param name="parents">The parents chromosomes.</param>
        /// <returns>The offspring (children) of the parents.</returns>
        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            IChromosome firstParent = parents[0];
            IChromosome secondParent = parents[1];
            IChromosome firstChild = firstParent.CreateNew();
            IChromosome secondChild = secondParent.CreateNew();

            for (int i = 0; i < firstParent.Length; i++)
            {
                if (RandomizationProvider.Current.GetDouble() < MixProbability)
                {
                    firstChild.ReplaceGene(i, firstParent.GetGene(i));
                    secondChild.ReplaceGene(i, secondParent.GetGene(i));
                }
                else
                {
                    firstChild.ReplaceGene(i, secondParent.GetGene(i));
                    secondChild.ReplaceGene(i, firstParent.GetGene(i));
                }
            }

            return new List<IChromosome> { firstChild, secondChild };
        }
        #endregion
    }
}
