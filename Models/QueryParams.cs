using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace flood_hackathon.Models
{
    public class QueryParams
    {
        [FromQuery(Name = "searchText")]
        public string SearchText { get; set; }

        [FromQuery(Name = "issues")]
        public IEnumerable<Issues> Issues { get; set; }

        [FromQuery(Name = "regions")]
        public IEnumerable<Regions> Regions { get; set; }

        [FromQuery(Name = "toolFunctions")]
        public IEnumerable<ToolFunctions> ToolFunctions { get; set; }
    }
}
