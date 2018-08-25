using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using flood_hackathon.Models;
using flood_hackathon.Models.Requests;

namespace flood_hackathon.DataAccess
{
    public interface ISearchIndex
    {
        Task MergeOrUploadSearchData(IEnumerable<ToolIndexContent> documents, CancellationToken cancellationToken);

        Task DeleteSearchData(string id, CancellationToken cancellationToken);

        Task<IEnumerable<ToolIndexContent>> QueryToolIndex(string query, CancellationToken cancellationToken);
    }
}