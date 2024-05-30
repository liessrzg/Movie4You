namespace Movie4You.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Release_Date { get; set; }
        public string Poster_Path { get; set; }
        public string Overview { get; set; }
        public int Vote_Count { get; set; }
        public float Popularity { get; set; }

        public double Vote_Average { get; set; }

    }
}
