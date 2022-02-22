using System.Diagnostics;
using GeneticAlgorithm.Implementations;
using GeneticAlgorithm.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GeneticAlgorithm.Web.Models;

namespace GeneticAlgorithm.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult InitializeGeneticAlgorithm(
            double[] limitX,
            double[] limitY,
            int populationMinSize,
            int populationMaxSize,
            float crossoverProbability,
            float mutationProbability
        )
        {
            IGeneticAlgorithmsForFindMinimum geneticAlgorithmsForFindMinimum = new GeneticAlgorithmsForFindMinimum();
            geneticAlgorithmsForFindMinimum.Initialize(
                limitX,
                limitY,
                populationMinSize,
                populationMaxSize,
                crossoverProbability,
                mutationProbability
            );
            return Ok();
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