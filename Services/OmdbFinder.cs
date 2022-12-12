using BlazorMovieClient.Models;

using System.Net.Http.Json;

namespace BlazorMovieClient.Services
{
    public sealed class OmdbFinder : IMovieFinder
    {
        public const string Key = "";

        public bool IsSearching { get; set; }
        private readonly IHttpClientFactory _httpClientFactory;

        public OmdbFinder(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Movie?> ByTitleAsync(string title)
        {
            string formatedTitle = title.Replace(" ", "+");
            return await ExecuteSearch($"?t={formatedTitle}");
        }

        public async Task<Movie?> ByImdbId(string id)
        {
            return await ExecuteSearch($"?i={id}");
        }

        private async Task<Movie?> ExecuteSearch(string resource)
        {
            IsSearching = true;

            try
            {
                HttpClient client = _httpClientFactory.CreateClient("Omdb");
                return await client.GetFromJsonAsync<Movie>($"{resource}&apikey={Key}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                IsSearching = false;
            }

            return null;
        }
    }
}
