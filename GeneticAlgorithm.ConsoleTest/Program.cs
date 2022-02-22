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
		
            FloatingPointChromosome chromosome = new FloatingPointChromosome(
                new double[] {minX,minY},
                new double[] {maxX,maxY},
                new int[] { 64,64},
                new int[] { 16,16});

            Population population = new Population(50, 100, chromosome);

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
            UniformCrossover crossover = new UniformCrossover(0.3f);
            FlipBitMutation mutation = new FlipBitMutation();
            FitnessThresholdTermination termination = new FitnessThresholdTermination(3.71828182846);

            Implementations.GeneticAlgorithm ga = new Implementations.GeneticAlgorithm(
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

            double latestFitness = 0.0;
            ga.GenerationRan += (sender, e) =>
            {
                FloatingPointChromosome bestChromosome = ga.BestChromosome as FloatingPointChromosome;
                double bestFitness = bestChromosome.Fitness.Value;
                
                double[] phenotypeCurrent = bestChromosome.ToFloatingPoints();
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
                    double[] phenotype = bestChromosome.ToFloatingPoints();

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