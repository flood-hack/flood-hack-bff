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
using flood_hackathon.Models.Requests;
using System.Threading;

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
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task MergeOrUploadSearchData(IEnumerable<ToolIndexContent> documents, CancellationToken cancellationToken)
        {
            var postBatch = IndexBatch.MergeOrUpload(documents);
            await SearchIndexClient.Documents.IndexAsync(postBatch, null, cancellationToken);
        }

        public async Task DeleteSearchData(string id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            await SearchIndexClient.Documents.CountAsync();

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ToolIndexContent>> QueryToolIndex(string query, CancellationToken cancellationToken)
        {
            try
            {
                DocumentSearchResult<ToolIndexContent> docs = await SearchIndexClient.Documents.SearchAsync<ToolIndexContent>(query, null, null, cancellationToken);
                return docs.Results.Cast<ToolIndexContent>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}