using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using GeneticAlgorithm.Implementations;
using GeneticAlgorithm.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GeneticAlgorithm.Web.Models;
using Newtonsoft.Json;
using static System.Single;

namespace GeneticAlgorithm.Web.Controllers
{
    public class HomeController : Controller
    {
        private GeneticAlgorithmsForFindMinimum geneticAlgorithmsForFindMinimum;
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult InitializeGeneticAlgorithm(
            double maximumX,
            double minimumX,
            double minimumY,
            double maximumY,
            int populationMinSize,
            int populationMaxSize,
            string crossoverProbability,
            string mutationProbability
        )
        {
            double[] limitX = {minimumX, maximumX};
            double[] limitY = {minimumY, maximumY};

            TryParse(crossoverProbability,out var mutationProb);

            TryParse(mutationProbability,out var crossoverProb);

            InputData inputData = new()
            {
                CrossoverProbability = crossoverProb,
                MutationProbability = mutationProb,
                X = limitX,
                Y = limitY,
                PopulationMaxSize = populationMaxSize,
                PopulationMinSize = populationMinSize
            };
           
            TempData["geneticAlgorithmsForFindMinimum"] = JsonConvert.SerializeObject(inputData);
            return NoContent();
        }
        

        [HttpPost]
        public JsonResult GetGenerationFitness()
        {
            if (TempData["geneticAlgorithmsForFindMinimum"] != null)
            {
                var inputData =  JsonConvert.DeserializeObject<InputData>((string)TempData["geneticAlgorithmsForFindMinimum"]);
                geneticAlgorithmsForFindMinimum = new GeneticAlgorithmsForFindMinimum();
            
                geneticAlgorithmsForFindMinimum.Initialize(
                    inputData.X,
                    inputData.Y,
                    inputData.PopulationMinSize,
                    inputData.PopulationMaxSize,
                    inputData.CrossoverProbability,
                    inputData.MutationProbability
                );
                GraphDataSet graphData = new()
                {
                    IsFilled = false,
                    LabelForDataSet = "Result from GA",
                    BorderColorForDataSet = "rgb(252, 3, 3)"
                };
                graphData.DataForDataSet = new List<ScatterConfig>();

                var geneneticResults = geneticAlgorithmsForFindMinimum.Run();

                foreach (var item in geneneticResults)
                {
                    graphData.DataForDataSet.Add(
                        new ScatterConfig()
                        {
                            X = item.ValueX,
                            Y = item.ValueY,
                        });
                }

                List<GraphDataSet> graphDataSets = new List<GraphDataSet>();
                graphDataSets.Add(graphData);
                GraphData graphDataSet = new GraphData()
                {
                    DataSets = graphDataSets
                };
                
                string jsonGraphdata = JsonConvert.SerializeObject(graphDataSet);

                return new JsonResult(jsonGraphdata);
            }
            else
            {
                return null;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}