using System;
using Sep6BackEnd.Controllers;

namespace Sep6BackEnd.BusinessLogic
{
    public interface IUsersBL
    {
        User CreateUser(string userName, string email, string password);
        string Login(string userName, string password);
        RatingObject SetFavoriteMovie(RatingObject ratingObject);
        RatingObject SetMovieRating(RatingObject ratingObject);
        int GetMovieRating(string userName, int movieId);
        bool GetFavoriteMovie(string userName, int movieId);
    }
}