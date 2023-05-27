// TODO - Check comment in Program.cs
namespace Sep6BackEnd.DataAccess.DomainClasses.DatabaseModels
{
    public class MovieRating
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int Rating { get; set; }

        public MovieRating(int userId, int movieId, int rating)
        {
            UserId = userId;
            MovieId = movieId;
            Rating = rating;
        }

        public MovieRating()
        {
            
        }
    }
}