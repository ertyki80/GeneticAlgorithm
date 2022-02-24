using Newtonsoft.Json;

namespace GeneticAlgorithm.Web.Models
{
    public class ScatterConfig
    {
        [JsonProperty("x")]
        public double X { get; set; }
        
        [JsonProperty("y")]
        public double Y { get; set; }
    }
}