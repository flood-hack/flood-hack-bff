using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using flood_hackathon.Models;
using flood_hackathon.Models.Requests;

namespace flood_hackathon.DataAccess
{
    public class SearchAdapter : ISearchAdapter
    {
        private ISearchIndex _searchIndex;

        public SearchAdapter(ISearchIndex searchIndex)
        {
            _searchIndex = searchIndex;
        }

        public async Task AddUpdateTool(IEnumerable<ToolIndexContent> addEditToolReqest, CancellationToken cancellationToken)
        {
            await _searchIndex.MergeOrUploadSearchData(addEditToolReqest, cancellationToken);
        }

        public async Task DeleteTool(string id, CancellationToken cancellationToken)
        {
            await _searchIndex.DeleteSearchData(id, cancellationToken);
        }

        public async Task<IEnumerable<ToolIndexContent>> QueryTools(QueryParams query, CancellationToken cancellationToken)
        {
            return await _searchIndex.QueryToolIndex(query, cancellationToken);
        }
    }
}