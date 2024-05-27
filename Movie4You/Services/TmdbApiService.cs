using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class TmdbApiService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "52c36a2508be2bc361bbda19a831dbdd";
    private const string BaseUrl = "https://api.themoviedb.org/3/";

    public TmdbApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MovieSearchResult> SearchMoviesAsync(string query)
    {
        string url = $"{BaseUrl}search/movie?api_key={ApiKey}&query={Uri.EscapeDataString(query)}";
        return await _httpClient.GetFromJsonAsync<MovieSearchResult>(url);
    }
}

public class MovieSearchResult
{
    public List<Movie> Results { get; set; }
}

public class Movie
{
    public string Title { get; set; }
    public string Release_Date { get; set; }
}
