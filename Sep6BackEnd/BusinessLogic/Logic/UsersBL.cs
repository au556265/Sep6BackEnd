using Sep6BackEnd.Controllers;
using Sep6BackEnd.DataAccess.DatabaseAccess;

namespace Sep6BackEnd.BusinessLogic
{
    public class UsersBL : IUsersBL
    {
        private IDatabaseAccess _databaseAccess;

        public UsersBL(DatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }


        public User CreateUser(string userName, string email, string password)
        {
            User user = _databaseAccess.CreateUser(userName, email, password);
            return user;
        }

        public string Login(string userName, string password)
        {
            var user = _databaseAccess.Login(userName, password);
            if (user != null)
            {
                return "Successfully logged in name:" + userName;
            }
            else
            {
                return "ERROR";
            }
        }

        public RatingObject SetFavoriteMovie(RatingObject ratingObject)
        {
            return _databaseAccess.SetFavoriteMovie(ratingObject);
        }

        public RatingObject SetMovieRating(RatingObject ratingObject)
        {
            return _databaseAccess.SetMovieRating(ratingObject);
        }

        public int GetMovieRating(string userName, int movieId)
        {
            var rating = _databaseAccess.GetMovieRating(userName, movieId);
            return rating;
        }

        public bool GetFavoriteMovie(string userName, int movieId)
        {
            return _databaseAccess.GetFavoriteMovie(userName, movieId);
        }
    }
}