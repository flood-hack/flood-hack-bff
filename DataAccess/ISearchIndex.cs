using System.Collections.Generic;
using System.Threading.Tasks;
using flood_hackathon.Models;

namespace flood_hackathon.DataAccess
{
    public interface ISearchIndex
    {
        Task MergeOrUploadSearchData(IEnumerable<ToolIndexContent> documents);

        Task DeleteSearchData(string id);

        Task<IEnumerable<ToolIndexContent>> QueryToolIndex(string query);
    }
}