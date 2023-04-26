using Sep6BackEnd.Controllers;

namespace Sep6BackEnd.DataAccess.DatabaseAccess
{
    public interface IDatabaseAccess 
    {
         User CreateUser(string userName, string email, string password);
         User Login(string userName, string password);
         RatingObject SetFavoriteMovie(RatingObject ratingObject);
         RatingObject SetMovieRating(RatingObject ratingObject);
         int GetMovieRating(string userName, int movieId);
         bool GetFavoriteMovie(string userName, int movieId);
    }
}