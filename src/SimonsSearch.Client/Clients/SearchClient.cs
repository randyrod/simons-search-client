using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimonsSearch.Client.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SimonsSearch.Client.Clients
{
    public class SearchClient
    {
        private readonly HttpClient _httpClient;

        public SearchClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<SearchResultModel>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<SearchResultModel>();
            }

            try
            {
                var response = await _httpClient.GetAsync($"search?query={query}");
                if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
                {
                    return new List<SearchResultModel>();
                }

                var content = await response.Content.ReadAsStringAsync();

                var parsed = JsonConvert.DeserializeObject<List<SearchResultModel>>(content);

                return parsed;
            }
            catch (HttpRequestException)
            {
                return new List<SearchResultModel>();
            }
        }
    }
}