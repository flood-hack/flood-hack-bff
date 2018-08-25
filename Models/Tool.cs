using System.Collections.Generic;

namespace flood_hackathon.Models
{
    public class Tool
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Issues> Issues { get; set; }

        public IEnumerable<Regions> Regions { get; set; }

        public IEnumerable<ToolFunctions> ToolFunctions { get; set; }
    }
}