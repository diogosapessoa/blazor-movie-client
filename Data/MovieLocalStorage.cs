using Blazored.LocalStorage;
using BlazorMovieClient.Models;

namespace BlazorMovieClient.Data
{
    public sealed class MovieLocalStorage : IMovieStore
    {
        private readonly ILocalStorageService _localStorage;

        public MovieLocalStorage(ILocalStorageService localStorageService)
        {
            _localStorage = localStorageService;
        }

        public async Task<Movie?> GetAync(string key)
        {
            return await _localStorage.GetItemAsync<Movie>(key);
        }

        public async IAsyncEnumerable<Movie> GetAllStartsWithAsync(string prefix)
        {
            IEnumerable<string> keys = await _localStorage.KeysAsync();
            IEnumerable<string> moviesKeys = keys.Where(k => k.StartsWith(prefix));
            foreach (string key in moviesKeys)
                yield return await _localStorage.GetItemAsync<Movie>(key);
        }

        public async Task SaveOrUpdateAsync(string key, Movie movie)
        {
            await _localStorage.SetItemAsync(key, movie);
        }

        public async Task RemoveAsync(string key)
        {
            await _localStorage.RemoveItemAsync(key);
        }
    }
}
