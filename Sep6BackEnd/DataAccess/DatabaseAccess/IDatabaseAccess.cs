using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6BackEnd.DataAccess.DomainClasses.DatabaseModels;

namespace Sep6BackEnd.DataAccess.DatabaseAccess
{
    public interface IDatabaseAccess 
    {
         Task<Users> CreateUser(string userName, string email, string password);
         Task<Users> Login(string userName, string password);
         Task<MovieFavorite> SetFavoriteMovie(MovieFavorite movieFavorite);
         Task<MovieRating> SetMovieRating(MovieRating movieRating);
         Task<int> GetMovieRating(int userId, int movieId);
         Task<bool> GetFavoriteMovie(int userId, int movieId);
         Task<IEnumerable<int>> GetAllMyFavoritesIds(int userId);
         Task<double> GetRatingSumFromUsers(int movieId);

         Task<int> GetCountedUsersRating(int movieId);
    }
}