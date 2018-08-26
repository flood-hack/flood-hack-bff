using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;

namespace flood_hackathon.Models
{
    [SerializePropertyNamesAsCamelCase]
    public class ToolIndexContent
    {
        [System.ComponentModel.DataAnnotations.Key]
        [IsRetrievable(true)]
        [IsFilterable]
        [JsonProperty("id")]
        public string Id { get; set; }

        [IsRetrievable(true)]
        [IsSearchable]
        [JsonProperty("name")]
        public string Name { get; set; }

        [IsRetrievable(true)]
        [IsSearchable]
        [JsonProperty("description")]
        public string Description { get; set; }

        [IsRetrievable(true)]
        [JsonProperty("url")]
        public string Url { get; set; }

        [IsRetrievable(true)]
        [IsFilterable]
        [JsonProperty("issues")]
        public IEnumerable<string> Issues { get; set; }

        [IsRetrievable(true)]
        [IsFilterable]
        [JsonProperty("regions")]
        public IEnumerable<string> Regions { get; set; }

        [IsRetrievable(true)]
        [IsFilterable]
        [JsonProperty("toolFunctions")]
        public IEnumerable<string> ToolFunctions { get; set; }
    }
}
