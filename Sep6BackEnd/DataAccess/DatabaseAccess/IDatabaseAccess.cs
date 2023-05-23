using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6BackEnd.Controllers;

namespace Sep6BackEnd.DataAccess.DatabaseAccess
{
    public interface IDatabaseAccess 
    {
         Users CreateUser(string userName, string email, string password);
         Users Login(string userName, string password);
         MovieFavorite SetFavoriteMovie(MovieFavorite movieFavorite);
         MovieRating SetMovieRating(MovieRating movieRating);
         int GetMovieRating(int userId, int movieId);
         bool GetFavoriteMovie(int userId, int movieId);
         Task<IEnumerable<int>> GetAllMyFavoritesIds(int userId);
         Task<double> GetRatingSumFromUsers(int movieId);

         Task<int> GetCountedUsersRating(int movieId);
    }
}