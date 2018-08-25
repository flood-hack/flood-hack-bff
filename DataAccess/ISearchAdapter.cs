using System.Collections.Generic;
using System.Threading.Tasks;
using flood_hackathon.Models;

namespace flood_hackathon.DataAccess
{
    public interface ISearchAdapter
    {
        Task AddUpdateTool(IEnumerable<ToolIndexContent> toolIndexContent);

        Task DeleteTool(string id);

        Task<IEnumerable<ToolIndexContent>> QueryTools(string query);
    }
}