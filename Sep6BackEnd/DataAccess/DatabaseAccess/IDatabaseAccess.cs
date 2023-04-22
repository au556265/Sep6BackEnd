using Sep6BackEnd.Controllers;

namespace Sep6BackEnd.DataAccess.DatabaseAccess
{
    public interface IDatabaseAccess 
    {
         User CreateUser(string userName, string email, string password);
         User Login(string userName, string password);
         void SetFavoriteMovie(string userName, string movieTitle);
         void SetMovieRating(string userName, string movieTitle, int rating);
         int GetMovieRating(string userName, string movieTitle);
    }
}