using Blazored.LocalStorage;

using BlazorMovieClient.Models;

using System.Net.Http.Json;

namespace BlazorMovieClient.Services
{
    public sealed class OmdbFinder : IMovieFinder
    {
        public bool IsSearching { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;

        public OmdbFinder(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
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
                string key = await _localStorage.GetItemAsStringAsync("apikey");
                HttpClient client = _httpClientFactory.CreateClient("Omdb");
                return await client.GetFromJsonAsync<Movie>($"{resource}&apikey={key}");
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
