using System;
using GeneticAlgorithm.Implementations.ChromosomeService;
using GeneticAlgorithm.Implementations.CrossoverService;
using GeneticAlgorithm.Implementations.FitnessService;
using GeneticAlgorithm.Implementations.Mutation;
using GeneticAlgorithm.Implementations.OperatorStrategy;
using GeneticAlgorithm.Implementations.OperatorStrategy.TaskExecution;
using GeneticAlgorithm.Implementations.Populations;
using GeneticAlgorithm.Implementations.SelectionService;

namespace GeneticAlgorithm.ConsoleTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            double maxX = 1.01;
            double minX = -1.01;
            double maxY = 3.01;
            double minY = 0.01;
		
            var chromosome = new FloatingPointChromosome(
                new double[] {minX,minY},
                new double[] {maxX,maxY},
                new int[] { 64,64},
                new int[] { 16,16});

            var population = new Population(50, 100, chromosome);

            var fitness = new FuncFitness((c) =>
            {
                var fc = c as FloatingPointChromosome;

                var values = fc.ToFloatingPoints();
                var x1 = values[0];
                var x2 = values[1];
                if (Math.Pow(x1, 2) == 0) return Double.NaN;
                if ((Math.Pow(x1, 2) - Math.Pow(x2, 2)) == 0) return Double.NaN;
                
                var result = Math.Sin(x1) + Math.Exp(Math.Cos(Math.Pow(x2,2)));
                return result;
            });

            var selection = new TournamentSelection();
            var crossover = new UniformCrossover(0.3f);
            var mutation = new FlipBitMutation();
            var termination = new FitnessThresholdTermination(3.71828182846);

            var ga = new Implementations.GeneticAlgorithm(
                population,
                fitness,
                selection,
                crossover,
                mutation);
            
            ga.CrossoverProbability = 0.8f;
            ga.MutationProbability = 0.05f;
            
            ga.TaskExecutor = new ParallelTaskExecutor();
            ga.Termination = termination;
            ga.OperatorsStrategy = new DefaultOperatorsStrategy();
            ga.Reinsertion = new ElitistReinsertion();
            Console.WriteLine("Generation: (x1, y1)");

            var latestFitness = 0.0;
            ga.GenerationRan += (sender, e) =>
            {
                var bestChromosome = ga.BestChromosome as FloatingPointChromosome;
                var bestFitness = bestChromosome.Fitness.Value;
                
                var phenotypeCurrent = bestChromosome.ToFloatingPoints();
                Console.WriteLine(
                    "Generation {0}: ({1},{2}) = {3}",
                    ga.GenerationsNumber,
                    phenotypeCurrent[0],
                    phenotypeCurrent[1],
                    bestFitness
                );
                if (Math.Abs(bestFitness - latestFitness) > 0.001)
                {
                    latestFitness = bestFitness;
                    var phenotype = bestChromosome.ToFloatingPoints();

                    Console.WriteLine(
                        "BEST Generation {0,2}: ({1},{2}) = {3}",
                        ga.GenerationsNumber,
                        phenotype[0],
                        phenotype[1],
                        bestFitness
                    );
                }
            };

            ga.Start();

            Console.ReadKey();
        }
    }
}