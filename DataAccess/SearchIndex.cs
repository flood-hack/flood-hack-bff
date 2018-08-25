using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using flood_hackathon.Models;

namespace flood_hackathon.DataAccess
{
    /// <summary>
    /// The data adapter for publishing data to the database.
    /// </summary>
    public class SearchIndex : ISearchIndex
    {
        private SearchIndexSettings _settings;
        private SearchServiceClient _searchServiceClient;
        private SearchServiceClient SearchClient
        {
            get
            {
                if (_searchServiceClient == null)
                {
                    _searchServiceClient = new SearchServiceClient(
                        _settings.ServiceName,
                        new SearchCredentials(_settings.PrimaryKey)
                    );
                }

                return _searchServiceClient;
            }
        }
        private ISearchIndexClient _searchIndexClient;
        private ISearchIndexClient SearchIndexClient
        {
            get
            {
                if (_searchIndexClient == null)
                {
                    _searchIndexClient = SearchClient.Indexes.GetClient(_settings.Index);
                }

                return _searchIndexClient;
            }
        }

        public SearchIndex(IOptions<SearchIndexSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task MergeOrUploadSearchData(IEnumerable<ToolIndexContent> documents)
        {
            var postBatch = IndexBatch.MergeOrUpload(documents);
            await SearchIndexClient.Documents.IndexAsync(postBatch);
        }

        public async Task DeleteSearchData(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ToolIndexContent>> QueryToolIndex(string query)
        {
            try
            {
                DocumentSearchResult<ToolIndexContent> docs = await SearchIndexClient.Documents.SearchAsync<ToolIndexContent>(query);
                return docs.Results.Cast<ToolIndexContent>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}