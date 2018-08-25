using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using flood_hackathon.Models;
using flood_hackathon.Models.Requests;

namespace flood_hackathon.DataAccess
{
    public interface ISearchAdapter
    {
        Task AddUpdateTool(IEnumerable<ToolIndexContent> request, CancellationToken cancellationToken);

        Task DeleteTool(string id, CancellationToken cancellationToken);

        Task<IEnumerable<ToolIndexContent>> QueryTools(string query, CancellationToken cancellationToken);
    }
}