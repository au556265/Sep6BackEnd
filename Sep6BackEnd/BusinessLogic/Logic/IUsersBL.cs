using Sep6BackEnd.Controllers;

namespace Sep6BackEnd.BusinessLogic
{
    public interface IUsersBL
    {
        User CreateUser(string userName, string email, string password);
        string Login(string userName, string password);
        void SetFavoriteMovie(string userName, string movieTitle);
        void SetMovieRating(string userName, string movieTitle, int rating);
        int GetMovieRating(string userName, string movieTitle);
    }
}