using System;
using System.Collections.ObjectModel;
using GeneticAlgorithm.Interfaces;
using NCalc;

namespace GeneticAlgorithm.Mathematic
{
    /// <summary>
    /// Function builder fitness.
    /// </summary>
    public class FunctionBuilderFitness : IFitness
    {
        #region Fields
        private readonly FunctionBuilderInput[] m_inputs;
        private readonly string[] m_parameterNames;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionBuilderFitness"/> class.
        /// </summary>
        /// <param name="inputs">The arguments values and expected results of the function.</param>
        public FunctionBuilderFitness(params FunctionBuilderInput[] inputs)
        {
            m_inputs = inputs;

            int parametersCount = m_inputs[0].Arguments.Count;
            AvailableOperations = FunctionBuilderChromosome.BuildAvailableOperations(parametersCount);
            m_parameterNames = FunctionBuilderChromosome.GetParameterNames(parametersCount);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the available operations.
        /// </summary>
        /// <value>The available operations.</value>
        public ReadOnlyCollection<string> AvailableOperations { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Performs the evaluation against the specified chromosome.
        /// </summary>
        /// <param name="chromosome">The chromosome to be evaluated.</param>
        /// <returns>The fitness of the chromosome.</returns>
        public double Evaluate(IChromosome chromosome)
        {
            FunctionBuilderChromosome c = chromosome as FunctionBuilderChromosome;
            string function = c.BuildFunction();
            double fitness = 0.0;

            foreach (FunctionBuilderInput input in m_inputs)
            {
                try
                {
                    double result = GetFunctionResult(function, input);
                    double diff = Math.Abs(result - input.ExpectedResult);

                    fitness += diff;
                }
                catch (Exception)
                {
                    return double.MinValue;
                }
            }

            return fitness * -1;
        }

        
        /// <summary>
        /// Gets the function result.
        /// </summary>
        /// <returns>The function result.</returns>
        /// <param name="function">The function.</param>
        /// <param name="input">The arguments values and expected results of the function.</param>
        public double GetFunctionResult(string function, FunctionBuilderInput input)
        {
            Expression expression = new NCalc.Expression(function);

            for (int i = 0; i < m_parameterNames.Length; i++)
            {
                expression.Parameters.Add(m_parameterNames[i], input.Arguments[i]);
            }

            object result = expression.Evaluate();

            return (double)result;
        }
        #endregion
    }
}
