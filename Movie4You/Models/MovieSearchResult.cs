
namespace Movie4You.Models
{


    public class MovieSearchResult
    {
        public int Page { get; set; }
        public int Total_Pages { get; set; }
        public List<Movie> Results { get; set; }
    }

}
