using BlazorMovieClient.Models;

namespace BlazorMovieClient.Data
{
    public interface IMovieStore
    {
        IAsyncEnumerable<Movie> GetAllStartsWithAsync(string prefix);
        Task<Movie?> GetAync(string key);
        Task RemoveAsync(string key);
        Task SaveOrUpdateAsync(string key, Movie movie);
    }
}
