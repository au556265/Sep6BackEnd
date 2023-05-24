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


        public async Task<Users> CreateUser(string userName, string email, string password)
        {
            Users user = await _databaseAccess.CreateUser(userName, email, password);
            return user;
        }

        public async Task<Users> Login(string userName, string password)
        { 
            var user = await _databaseAccess.Login(userName, password);
            return user;
        }

        public async Task<MovieFavorite> SetFavoriteMovie(MovieFavorite movieFavorite)
        {
            return await _databaseAccess.SetFavoriteMovie(movieFavorite);
        }

        public async Task<MovieRating> SetMovieRating(MovieRating movieRating)
        {
            return await _databaseAccess.SetMovieRating(movieRating);
        }

        public async Task<int> GetMovieRating(int userId, int movieId)
        {
            var rating = await _databaseAccess.GetMovieRating(userId, movieId);
            return rating;
        }

        public async Task<bool> GetFavoriteMovie(int userId, int movieId)
        {
            return await _databaseAccess.GetFavoriteMovie(userId, movieId);
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