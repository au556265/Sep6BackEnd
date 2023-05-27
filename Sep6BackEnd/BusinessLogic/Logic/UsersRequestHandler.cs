using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6BackEnd.DataAccess.DatabaseAccess;
using Sep6BackEnd.DataAccess.DomainClasses.APIModels;
using Sep6BackEnd.DataAccess.DomainClasses.DatabaseModels;
using Sep6BackEnd.DataAccess.TMDBAccess;

namespace Sep6BackEnd.BusinessLogic.Logic
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
            try
            {
                Users user = await _databaseAccess.CreateUser(userName, email, password);
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Users> Login(string userName, string password)
        {
            try
            {
                var user = await _databaseAccess.Login(userName, password);
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<MovieFavorite> SetFavoriteMovie(MovieFavorite movieFavorite)
        {
            try
            {
                return await _databaseAccess.SetFavoriteMovie(movieFavorite);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<MovieRating> SetMovieRating(MovieRating movieRating)
        {
            try
            {
                return await _databaseAccess.SetMovieRating(movieRating);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> GetMovieRating(int userId, int movieId)
        {
            try
            {
                var rating = await _databaseAccess.GetMovieRating(userId, movieId);
                return rating;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<bool> GetFavoriteMovie(int userId, int movieId)
        {
            try
            {
                return await _databaseAccess.GetFavoriteMovie(userId, movieId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<Movie>> GetAllMyFavoritesMovies(int userId)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}