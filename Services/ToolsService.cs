using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using flood_hackathon.DataAccess;
using flood_hackathon.Models;
using flood_hackathon.Models.Requests;

namespace flood_hackathon.Services
{
    public class ToolsService
    {
        private ISearchAdapter _searchAdapter;
        public ToolsService(ISearchAdapter searchAdapter)
        {
            _searchAdapter = searchAdapter;
        }

        public async Task<ToolIndexContent> GetTool(string id, CancellationToken CancellationToken)
        {
            return await Task.FromResult<ToolIndexContent>(new ToolIndexContent());
        }

        public async Task<IEnumerable<ToolIndexContent>> GetTools(QueryParams query, CancellationToken CancellationToken)
        {
            return await Task.FromResult<IEnumerable<ToolIndexContent>>(new List<ToolIndexContent>());
        }

        public async Task AddTool(AddEditToolRequest request, CancellationToken CancellationToken)
        {
            var toUpdate = new List<ToolIndexContent>() { MapAddRequest(request) };
            await _searchAdapter.AddUpdateTool(toUpdate, CancellationToken);
        }

        public async Task UpdateTool(string id, AddEditToolRequest request, CancellationToken cancellationToken)
        {

        }

        public async Task DeleteTool(string id, CancellationToken cancellationToken)
        {
            await _searchAdapter.DeleteTool(id, cancellationToken);
        }

        #region Mappers

        private ToolIndexContent MapAddRequest(AddEditToolRequest request)
        {
            return new ToolIndexContent()
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                Issues = request.Issues.Select(i => i.ToString()),
                Regions = request.Regions.Select(i => i.ToString()),
                ToolFunctions = request.ToolFunctions.Select(i => i.ToString())
            };
        }
        #endregion
    }
}