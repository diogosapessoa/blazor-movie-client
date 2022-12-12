using System.Text.Json.Serialization;

namespace BlazorMovieClient.Models;

public sealed class Movie
{
    private string? imdbId;
    [JsonPropertyName("imdbID")]
    public string? ImdbID
    {
        get => imdbId?.ToUpperInvariant();
        set => imdbId = value;
    }

    public string? Title { get; set; }
    public string? Plot { get; set; }
    public string? Year { get; set; }
    public string? Genre { get; set; }
    public string? Poster { get; set; }
    public string? MetaScore { get; set; }

    public bool Watched { get; set; }

    [JsonIgnore]
    public string? WatchedText => Watched ? "Yes" : "No";

    [JsonIgnore]
    public string TitleAndYear => $"{Title} {Year}";

    [JsonIgnore]
    public int Stars
    {
        get
        {
            if (string.IsNullOrWhiteSpace(MetaScore) || MetaScore.ToLowerInvariant() == "n/a")
                return 0;

            return (int)Math.Ceiling(Convert.ToInt32(MetaScore) / 20f);
        }
    }

    [JsonIgnore]
    public string Rating => $"{MetaScore}/100";

    [JsonIgnore]
    public string? PlotTruncated => Plot?[..Math.Min(Plot?.Length ?? 0, 165)];

    public override string ToString()
    {
        return $"ImdbID={ImdbID}, Year ={Year}, MetaScore={MetaScore}, Title={Title}";
    }
}
