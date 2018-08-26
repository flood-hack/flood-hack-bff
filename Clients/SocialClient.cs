using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using flood_hackathon.Models;
using Newtonsoft.Json.Linq;

namespace flood_hackathon.Clients
{
  public class SocialClient
  {
    private HttpClient _httpClient;
    private SocialSettings _settings;

    private string _token;

    public SocialClient(IOptions<SocialSettings> settings)
    {
      _settings = settings.Value;
      _httpClient = new HttpClient();
      _httpClient.BaseAddress = new System.Uri(_settings.URI);
      // _httpClient.DefaultRequestHeaders.Authorization =
      //   new AuthenticationHeaderValue(
      //     "Bearer",
      //     _settings.AccessToken
      //   );
    }

    private async Task AddHeaders() 
    {
      if (String.IsNullOrEmpty(_token)) {

        var bearerClient = new HttpClient();
        bearerClient.DefaultRequestHeaders.Authorization =
          new AuthenticationHeaderValue(
            "Basic",
            Convert.ToBase64String(
              System.Text.Encoding.UTF8.GetBytes(
                string.Format(
                  System.Globalization.CultureInfo.InvariantCulture,
                  "{0}:{1}",
                  _settings.ConsumerKey,
                  _settings.ConsumerSecret
                )
              )
            )
          );

        var request = await bearerClient.PostAsync(
          "https://api.twitter.com/oauth2/token",
          new StringContent(
            "grant_type=client_credentials",
             Encoding.UTF8,
            "application/x-www-form-urlencoded"
          )
        );

        var response = await request.Content.ReadAsStringAsync();
        var json = JObject.Parse(response);

        _token = json.Property("access_token").Value.ToString();
        _httpClient.DefaultRequestHeaders.Authorization =
          new AuthenticationHeaderValue(
            "Bearer",
            _token
          );
      }
    }

    public async Task<IActionResult> GetSocial(QueryString qs)
    {
        return await GetEndpoint("search/tweets.json", qs);
    }

    public async Task<IActionResult> GetGeo(QueryString qs)
    {
        return await GetEndpoint("geo/search.json", qs);
    }

    private async Task<IActionResult> GetEndpoint(string endpoint, QueryString qs)
    {
        await AddHeaders();
        var response = await _httpClient.GetAsync(endpoint + qs.ToString());
        response.EnsureSuccessStatusCode();

        return new ObjectResult(await response.Content.ReadAsStringAsync());
    }
  }
}
