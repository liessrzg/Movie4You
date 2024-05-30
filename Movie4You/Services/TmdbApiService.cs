using Movie4You.Models;
using System.Net.Http.Json;

public class TmdbApiService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "52c36a2508be2bc361bbda19a831dbdd";
    private const string BaseUrl = "https://api.themoviedb.org/3/";

    public TmdbApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MovieSearchResult> SearchMoviesAsync(string query, string category = null, string genreId = null, int page = 1)
    {
        string url;

        if (!string.IsNullOrEmpty(query))
        {
            url = $"{BaseUrl}search/movie?api_key={ApiKey}&query={Uri.EscapeDataString(query)}&page={page}";
            if (!string.IsNullOrEmpty(genreId))
            {
                url += $"&with_genres={genreId}";
            }
        }
        else
        {
            url = $"{BaseUrl}discover/movie?api_key={ApiKey}&page={page}&sort_by=popularity.desc";

            if (!string.IsNullOrEmpty(category))
            {
                if (category == "upcoming")
                {
                    url += $"&primary_release_date.gte={DateTime.UtcNow:yyyy-MM-dd}";
                }
                else
                {
                    url += $"&with_{category}=true"; // This line ensures that the category filter is added to the URL
                }
            }

            if (!string.IsNullOrEmpty(genreId))
            {
                url += $"&with_genres={genreId}";
            }
        }

        return await _httpClient.GetFromJsonAsync<MovieSearchResult>(url);
    }
    public async Task<MovieSearchResult> GetMovieVideosAsync(int movieId)
    {
        string url = $"{BaseUrl}movie/{movieId}/videos?api_key={ApiKey}";
        return await _httpClient.GetFromJsonAsync<MovieSearchResult>(url);
    }

    public async Task<MovieSearchResult> GetMoviesByCategoryAsync(string category, int page = 1)
    {
        string url = $"{BaseUrl}movie/{category}?api_key={ApiKey}&page={page}";
        return await _httpClient.GetFromJsonAsync<MovieSearchResult>(url);
    }

    public async Task<MovieSearchResult> GetMoviesByGenreAsync(string genreId, int page = 1)
    {
        string url = $"{BaseUrl}discover/movie?api_key={ApiKey}&with_genres={genreId}&page={page}&sort_by=popularity.desc";
        return await _httpClient.GetFromJsonAsync<MovieSearchResult>(url);
    }

    public async Task<Movie> GetMovieDetailsAsync(string title)
    {
        string url = $"{BaseUrl}search/movie?api_key={ApiKey}&query={Uri.EscapeDataString(title)}";
        var searchResult = await _httpClient.GetFromJsonAsync<MovieSearchResult>(url);
        return searchResult?.Results?.FirstOrDefault();
    }
}
