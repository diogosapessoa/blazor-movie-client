using BlazorMovieClient.Models;

namespace BlazorMovieClient.Services
{
    public interface IMovieFinder
    {
        bool IsSearching { get; set; }

        Task<Movie?> ByImdbId(string id);
        Task<Movie?> ByTitleAsync(string title);
    }
}