using System.Collections.Generic;
using System.ComponentModel;
using GeneticAlgorithm.Implementations.ChromosomeService;
using GeneticAlgorithm.Interfaces;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm.Implementations.CrossoverService
{

    [DisplayName("Three Parent")]
    public class ThreeParentCrossover : CrossoverBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ThreeParentCrossover"/> class.
        /// </summary>
        public ThreeParentCrossover()
            : base(3, 1)
        {
        }
        #endregion

        #region Methods        
        /// <summary>
        /// Performs the cross with specified parents generating the children.
        /// </summary>
        /// <param name="parents">The parents chromosomes.</param>
        /// <returns>
        /// The offspring (children) of the parents.
        /// </returns>
        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            IChromosome parent1 = parents[0];
            Gene[] parent1Genes = parent1.GetGenes();
            Gene[] parent2Genes = parents[1].GetGenes();
            Gene[] parent3Genes = parents[2].GetGenes();
            IChromosome offspring = parent1.CreateNew();
            Gene parent1Gene;

            for (int i = 0; i < parent1.Length; i++)
            {
                parent1Gene = parent1Genes[i];

                if (parent1Gene == parent2Genes[i])
                {
                    offspring.ReplaceGene(i, parent1Gene);
                }
                else
                {
                    offspring.ReplaceGene(i, parent3Genes[i]);
                }
            }

            return new List<IChromosome>() { offspring };
        }
        #endregion
    }
}
