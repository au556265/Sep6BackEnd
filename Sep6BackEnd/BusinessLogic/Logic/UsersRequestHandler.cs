using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sep6BackEnd.Controllers;
using Sep6BackEnd.DataAccess.DatabaseAccess;
using Sep6BackEnd.DataAccess.IMDBAccess;
using Sep6BackEnd.DataAccess.TMDBAccess;

namespace Sep6BackEnd.BusinessLogic
{
    public class UsersRequestHandler : IUsersRequestHandler
    {
        private readonly IDatabaseAccess _databaseAccess;
        private readonly ITmdbAccess _tmdbAccess;

        public UsersRequestHandler(DatabaseAccess databaseAccess, TmdbAccess tmdbAccess)
        {
            _databaseAccess = databaseAccess;
            _tmdbAccess = tmdbAccess;
        }


        public Users CreateUser(string userName, string email, string password)
        {
            Users users = _databaseAccess.CreateUser(userName, email, password);
            return users;
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

        public MovieFavorite SetFavoriteMovie(MovieFavorite movieFavorite)
        {
            return _databaseAccess.SetFavoriteMovie(movieFavorite);
        }

        public MovieRating SetMovieRating(MovieRating movieRating)
        {
            return _databaseAccess.SetMovieRating(movieRating);
        }

        public int GetMovieRating(int userId, int movieId)
        {
            var rating = _databaseAccess.GetMovieRating(userId, movieId);
            return rating;
        }

        public bool GetFavoriteMovie(int userId, int movieId)
        {
            return _databaseAccess.GetFavoriteMovie(userId, movieId);
        }

        public async Task<IEnumerable<Movie>> GetAllMyFavoritesMovies(int userId)
        {
            //start fetching favorite ids local database
            var myFavoriteMoviesIds = await _databaseAccess.GetAllMyFavoritesIds(userId);
            
            var myfavorites = new List<Movie>();
            //fetch favorite movies from TMDBAPI
            foreach (var id in myFavoriteMoviesIds)
            {
                var movie = await _tmdbAccess.GetMovie(id);
                myfavorites.Add(movie);
            }

            return myfavorites;
        }
        
    }
}