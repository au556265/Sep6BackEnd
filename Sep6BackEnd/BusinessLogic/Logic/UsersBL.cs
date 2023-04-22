using Sep6BackEnd.Controllers;
using Sep6BackEnd.DataAccess.DatabaseAccess;

namespace Sep6BackEnd.BusinessLogic
{
    public class UsersBL : IUsersBL
    {
        private IDatabaseAccess _databaseAccess;

        public UsersBL()
        {
            _databaseAccess = new DatabaseAccess();
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

        public void SetFavoriteMovie(string userName, string movieTitle)
        {
            _databaseAccess.SetFavoriteMovie(userName, movieTitle);
        }

        public void SetMovieRating(string userName, string movieTitle, int rating)
        {
            _databaseAccess.SetMovieRating(userName, movieTitle, rating);
        }

        public int GetMovieRating(string userName, string movieTitle)
        {
            var rating = _databaseAccess.GetMovieRating(userName, movieTitle);
            return rating;
        }
    }
}