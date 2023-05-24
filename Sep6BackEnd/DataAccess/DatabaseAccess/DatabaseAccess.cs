using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.Controllers;

namespace Sep6BackEnd.DataAccess.DatabaseAccess
{
    public class DatabaseAccess : IDatabaseAccess
    {
        private readonly Keys keys;
        public DatabaseAccess(Keys keys)
        {
            this.keys = keys;
        }
        
        public async Task<Users> CreateUser(string userName, string email, string password)
        {
            try
            {
                using var dbSqlConnection = new SqlConnection(keys.DBSKEY);
                const string query =
                    @"IF NOT EXISTS ( SELECT 1 FROM Users WHERE Username = @username OR Email =@email) 
                    BEGIN
                    INSERT INTO Users (Username, Password, Email) OUTPUT INSERTED.* VALUES (@userName, @password, @email)
                    END";

                var output = await dbSqlConnection.QuerySingleAsync<Users>(query, new {userName, password, email});
                
                return output;

            }
            catch (Exception e)
            {
                return null;
            }

        }

        public async Task<Users> Login(string userName, string password)
        {
            try
            {
                using var dbSqlConnection = new SqlConnection(keys.DBSKEY);
                const string query = @"SELECT 1 FROM Users WHERE Username= @userName AND Password= @password ";
                var results = await dbSqlConnection.QueryFirstOrDefaultAsync<Users>(query, new {userName, password});
                return results;
                
            }
            catch (Exception e)
            {
                return null;
            }
           
        }

        public async Task<MovieFavorite> SetFavoriteMovie(MovieFavorite movieFavorite)
        {
            int userId = movieFavorite.UserId;
            int movieId = movieFavorite.MovieId;
            int favorite = movieFavorite.Favorite;
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query = @"BEGIN TRAN
IF EXISTS (select * from MovieFavorite where UserId = @userId AND MovieId = @movieId)
begin
   update MovieFavorite set Favorite = @favorite
   where UserId = @userId AND MovieId = @movieId
end
else
begin
   insert into MovieFavorite (UserId, MovieId, Favorite)
   values (@userId, @movieId, @favorite)
end
commit tran";
                await dbSqlConnection.QueryAsync(query, new {userId, movieId, favorite});
            }

            return new MovieFavorite();
        }
        
        

        public async Task<MovieRating> SetMovieRating( MovieRating movieRating)
        {
            int userId = movieRating.UserId;
            int movieId = movieRating.MovieId;
            int rating = movieRating.Rating;
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query = @"BEGIN TRAN
IF EXISTS (select * from MovieRating where UserId = @userId AND MovieId = @movieId)
begin
   update MovieRating set Rating = @rating
   where UserId = @userId AND MovieId = @movieId
end
else
begin
   insert into MovieRating (UserId, MovieId, Rating)
   values (@userId, @movieId, @rating)
end
commit tran";
                await dbSqlConnection.QueryAsync(query, new {userId, movieId, rating});
            }

            return new MovieRating();
        }

        public async Task<int> GetMovieRating(int userId, int movieId)
        {
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query = @"SELECT Rating FROM MovieRating WHERE UserId= @userId AND MovieId= @movieId";
                var rating = await dbSqlConnection.QueryFirstOrDefaultAsync<int>(query, new {userId, movieId});
                
                return rating;
            }
        }

        public async Task<bool> GetFavoriteMovie(int userId, int movieId)
        {
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query = @"SELECT Favorite FROM MovieFavorite WHERE UserId= @userId AND MovieId= @movieId";
                var favorite = await dbSqlConnection.QueryFirstOrDefaultAsync<bool>(query, new {userId, movieId});
                
                return favorite;
            }
        }

      
        public async Task <IEnumerable<int>>GetAllMyFavoritesIds(int userId)
        {
            using var dbSqlConnection = new SqlConnection(keys.DBSKEY);
            
            const string query = @"SELECT MovieId FROM MovieFavorite WHERE UserId= @userId AND Favorite = 1";
            var allMyFavorites =  await dbSqlConnection.QueryAsync<int>(query, new {userId});
            return allMyFavorites;
            
        }

        public async Task<double> GetRatingSumFromUsers(int movieId)
        {
            await using var dbSqlConnection = new SqlConnection(keys.DBSKEY);
            
            const string query = @"SELECT SUM(Rating) FROM MovieRating WHERE MovieId= @movieId";
            var usersRatingSum = await dbSqlConnection.QuerySingleOrDefaultAsync<double?>(query, new {movieId});
            return usersRatingSum ?? 0.0;
        }

        public async Task<int> GetCountedUsersRating(int movieId)
        {
            await using var dbSqlConnection = new SqlConnection(keys.DBSKEY);
            const string query = @"SELECT Count(UserId) FROM MovieRating WHERE MovieId= @movieId";
            var usersCounted = await dbSqlConnection.QuerySingleOrDefaultAsync<int?>(query, new {movieId});
            return usersCounted ?? 0;
        }
    }
}