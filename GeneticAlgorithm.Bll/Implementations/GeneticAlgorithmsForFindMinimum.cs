using System;
using System.Collections.Generic;
using GeneticAlgorithm.Implementations.ChromosomeService;
using GeneticAlgorithm.Implementations.CrossoverService;
using GeneticAlgorithm.Implementations.FitnessService;
using GeneticAlgorithm.Implementations.Mutation;
using GeneticAlgorithm.Implementations.OperatorStrategy;
using GeneticAlgorithm.Implementations.OperatorStrategy.TaskExecution;
using GeneticAlgorithm.Implementations.Populations;
using GeneticAlgorithm.Implementations.SelectionService;
using GeneticAlgorithm.Interfaces;
using GeneticAlgorithm.Models;

namespace GeneticAlgorithm.Implementations
{
    public class GeneticAlgorithmsForFindMinimum: IGeneticAlgorithmsForFindMinimum
    {
        private GeneticAlgorithm GeneticAlgorithm { get; set; }
        
        
        public void Initialize(
            double[] limitX,
            double[] limitY,
            int populationMinSize,
            int populationMaxSize,
            float crossoverProbability,
            float mutationProbability
        )
        {

            FloatingPointChromosome chromosome = new FloatingPointChromosome(
                new double[] {limitX[0],limitY[0]},
                new double[] {limitX[1],limitY[0]},
                new int[] { 64,64},
                new int[] { 16,16});

            Population population = new Population(populationMinSize, populationMaxSize, chromosome);

            FuncFitness fitness = new FuncFitness((c) =>
            {
                FloatingPointChromosome fc = c as FloatingPointChromosome;

                double[] values = fc.ToFloatingPoints();
                double x1 = values[0];
                double x2 = values[1];
                if (Math.Pow(x1, 2) == 0) return Double.NaN;
                if ((Math.Pow(x1, 2) - Math.Pow(x2, 2)) == 0) return Double.NaN;
                
                double result = Math.Sin(x1) + Math.Exp(Math.Cos(Math.Pow(x2,2)));
                return result;
            });

            TournamentSelection selection = new TournamentSelection();
            UniformCrossover crossover = new UniformCrossover(crossoverProbability);
            FlipBitMutation mutation = new FlipBitMutation();
            FitnessStagnationTermination termination = new FitnessStagnationTermination();

            GeneticAlgorithm ga = new GeneticAlgorithm(
                population,
                fitness,
                selection,
                crossover,
                mutation);
            
            
            ga.CrossoverProbability = crossoverProbability;
            ga.MutationProbability = mutationProbability;
            
            ga.TaskExecutor = new ParallelTaskExecutor();
            ga.Termination = termination;
            ga.OperatorsStrategy = new DefaultOperatorsStrategy();
            ga.Reinsertion = new ElitistReinsertion();
            GeneticAlgorithm = ga;
            
        }

        public List<GeneticAlgorithmResult> Run()
        {
            List<GeneticAlgorithmResult> geneticAlgorithmResults = new List<GeneticAlgorithmResult>();
            double latestFitness = 0.0;
            GeneticAlgorithm.GenerationRan += (sender, e) =>
            {
                FloatingPointChromosome bestChromosome = GeneticAlgorithm.BestChromosome as FloatingPointChromosome;
                double currentFitness = bestChromosome.Fitness.Value;
                
                double[] phenotypeCurrent = bestChromosome.ToFloatingPoints();
                
                geneticAlgorithmResults.Add(new GeneticAlgorithmResult()
                {
                    GenerationNumber = GeneticAlgorithm.GenerationsNumber,
                    ValueX =  phenotypeCurrent[0],
                    ValueY =  phenotypeCurrent[1],
                    Fitness = currentFitness
                });
            };
            return geneticAlgorithmResults;
        }
    }
    
    
}