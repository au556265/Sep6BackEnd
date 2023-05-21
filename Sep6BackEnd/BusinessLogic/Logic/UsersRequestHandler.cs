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

        public async Task<IEnumerable<Movie>> GetAllMyFavoritesMovies(string userName)
        {
            //start fetching favorite ids local database
            var myFavoriteMoviesIds = await _databaseAccess.GetAllMyFavoritesIds(userName);
            
            var myfavorites = new List<Movie>();
            //fetch favorite movies from TMDBAPI
            foreach (var id in myFavoriteMoviesIds)
            {
                var movie = await _tmdbAccess.GetMovie(id);
                myfavorites.Add(movie);
            }

            return myfavorites;
        }

        public async Task<double> GetAverageRatingTotal(int movieId)
        {
            var ratingSumFromUsers= await _databaseAccess.GetRatingSumFromUsers(movieId);
            var countedUsersRating = await _databaseAccess.GetCountedUsersRating(movieId);
            
            var movie = await _tmdbAccess.GetMovie(movieId);
            
            var tmdbRatingCount = movie.vote_count;
            var tmdbRatingAverage = movie.vote_average;
            
            var tmdbRatingSum = tmdbRatingAverage * tmdbRatingCount;

            var totalVotingAverage = (tmdbRatingSum+ratingSumFromUsers)/(tmdbRatingCount+countedUsersRating);

            return totalVotingAverage;
        }
    }
}