using System.Collections.Generic;
using System.Linq;

namespace flood_hackathon.Models.Requests
{
    public class AddEditToolRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Issues> Issues { get; set; } = Enumerable.Empty<Issues>();

        public IEnumerable<Regions> Regions { get; set; } = Enumerable.Empty<Regions>();

        public IEnumerable<ToolFunctions> ToolFunctions { get; set; } = Enumerable.Empty<ToolFunctions>();
    }
}