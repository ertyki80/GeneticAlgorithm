using System.Collections.Generic;
using Newtonsoft.Json;

namespace GeneticAlgorithm.Web.Models
{
    public class GraphData
    {
        
        [JsonProperty("datasets")]
        public List<GraphDataSet> DataSets { get; set; }
    }
}