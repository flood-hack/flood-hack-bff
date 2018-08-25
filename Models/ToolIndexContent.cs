using Microsoft.Azure.Search;

namespace flood_hackathon.Models
{
    public class ToolIndexContent
    {
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        public string Id { get; set; }
    }
}