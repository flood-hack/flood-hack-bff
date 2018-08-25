using System.Collections.Generic;
using System.Threading.Tasks;
using flood_hackathon.Models;

namespace flood_hackathon.DataAccess
{
    public class SearchAdapter : ISearchAdapter
    {
        private SearchIndex _searchIndex;

        public SearchAdapter(SearchIndex searchIndex)
        {
            _searchIndex = searchIndex;
        }

        public async Task AddUpdateTool(IEnumerable<ToolIndexContent> toolIndexContent)
        {
            await _searchIndex.MergeOrUploadSearchData(toolIndexContent);
        }

        public async Task DeleteTool(string id)
        {
            await _searchIndex.DeleteSearchData(id);
        }

        public async Task<IEnumerable<ToolIndexContent>> QueryTools(string query)
        {
            return await _searchIndex.QueryToolIndex(query);
        }
    }
}