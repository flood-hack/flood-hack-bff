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
using System.Text;

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

        public async Task<IEnumerable<ToolIndexContent>> QueryToolIndex(QueryParams query, CancellationToken cancellationToken)
        {
            SearchParameters parameters = GenerateSearchParameters(query);
            try
            {
                DocumentSearchResult<ToolIndexContent> docs = await SearchIndexClient.Documents.SearchAsync<ToolIndexContent>(query.SearchText, parameters, null, cancellationToken);
                return docs.Results.Select(doc =>
                {
                    return new ToolIndexContent()
                    {
                        Id = doc.Document.Id,
                        Name = doc.Document.Name,
                        Description = doc.Document.Description,
                        Issues = doc.Document.Issues,
                        ToolFunctions = doc.Document.ToolFunctions,
                        Regions = doc.Document.Regions,
                        Url = doc.Document.Url
                    };
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private SearchParameters GenerateSearchParameters(QueryParams query)
        {
            var searchParams = new SearchParameters();

            if (String.IsNullOrEmpty(query.SearchText))
            {
                searchParams.SearchFields = new[] { "name", "description" };
            }

            var filterString = new StringBuilder();

            if (query.Issues?.Count() > 0)
            {
                filterString.Append("issues/any(i: search.in(i, '");
                foreach (var issue in query.Issues)
                {
                    filterString.Append($"{issue.ToString()}, ");
                }
                filterString.Length = filterString.Length - 2;
                filterString.Append("')) or ");

            }

            if (query.Regions?.Count() > 0)
            {
                filterString.Append("regions/any(r: search.in(r, '");
                foreach (var region in query.Regions)
                {
                    filterString.Append($"{region.ToString()}|");
                }

                filterString.Length = filterString.Length - 2;
                filterString.Append("')) or ");
            }

            if (query.ToolFunctions?.Count() > 0)
            {
                filterString.Append("toolFunctions/any(tf: search.in(tf, '");
                foreach (var tf in query.ToolFunctions)
                {
                    filterString.Append($"{tf.ToString()}|");
                }
                filterString.Length = filterString.Length - 2;
                filterString.Append("')) or ");
            }

            if (filterString.Length > 0)
            {
                filterString.Length = filterString.Length - 4;
                searchParams.Filter = filterString.ToString();
            }

            return searchParams;
        }
    }
}