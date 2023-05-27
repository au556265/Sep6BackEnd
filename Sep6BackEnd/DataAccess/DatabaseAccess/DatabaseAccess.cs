using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Sep6BackEnd.DataAccess.DomainClasses.DatabaseModels;

namespace Sep6BackEnd.DataAccess.DatabaseAccess
{
    public class DatabaseAccess : IDatabaseAccess
    {
        private readonly Keys _keys;
        public DatabaseAccess(Keys keys)
        {
            _keys = keys;
        }
        
        public async Task<Users> CreateUser(string userName, string email, string password)
        {
            try
            {
                using var dbSqlConnection = new SqlConnection(_keys.DBSKEY);
                const string query =
                    @"IF NOT EXISTS ( SELECT * FROM Users WHERE Username = @username OR Email =@email) 
                    BEGIN
                    INSERT INTO Users (Username, Password, Email) OUTPUT INSERTED.* VALUES (@userName, @password, @email)
                    END";

                var output = await dbSqlConnection.QuerySingleAsync<Users>(query, new {userName, password, email});
                return output;

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
                using var dbSqlConnection = new SqlConnection(_keys.DBSKEY);
                const string query = @"SELECT * FROM Users WHERE Username= @userName AND Password= @password ";
                var results = await dbSqlConnection.QueryFirstOrDefaultAsync<Users>(query, new {userName, password});
                return results;
                
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
                int userId = movieFavorite.UserId;
                int movieId = movieFavorite.MovieId;
                int favorite = movieFavorite.Favorite;
                using (var dbSqlConnection = new SqlConnection(_keys.DBSKEY))
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<MovieRating> SetMovieRating( MovieRating movieRating)
        {
            try
            {
                int userId = movieRating.UserId;
                int movieId = movieRating.MovieId;
                int rating = movieRating.Rating;
                using (var dbSqlConnection = new SqlConnection(_keys.DBSKEY))
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

                //returning MovieRating
                return new MovieRating(userId, movieId, rating);
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
                using (var dbSqlConnection = new SqlConnection(_keys.DBSKEY))
                {
                    const string query = @"SELECT Rating FROM MovieRating WHERE UserId= @userId AND MovieId= @movieId";
                    var rating = await dbSqlConnection.QueryFirstOrDefaultAsync<int>(query, new {userId, movieId});
                
                    return rating;
                }
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
                using (var dbSqlConnection = new SqlConnection(_keys.DBSKEY))
                {
                    const string query = @"SELECT Favorite FROM MovieFavorite WHERE UserId= @userId AND MovieId= @movieId";
                    var favorite = await dbSqlConnection.QueryFirstOrDefaultAsync<bool>(query, new {userId, movieId});
                
                    return favorite;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task <IEnumerable<int>>GetAllMyFavoritesIds(int userId)
        {
            try
            {
                using var dbSqlConnection = new SqlConnection(_keys.DBSKEY);
            
                const string query = @"SELECT MovieId FROM MovieFavorite WHERE UserId= @userId AND Favorite = 1";
                var allMyFavorites =  await dbSqlConnection.QueryAsync<int>(query, new {userId});
                return allMyFavorites;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<double> GetRatingSumFromUsers(int movieId)
        {
            try
            {
                await using var dbSqlConnection = new SqlConnection(_keys.DBSKEY);
            
                const string query = @"SELECT SUM(Rating) FROM MovieRating WHERE MovieId= @movieId";
                var usersRatingSum = await dbSqlConnection.QuerySingleOrDefaultAsync<double?>(query, new {movieId});
                return usersRatingSum ?? 0.0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> GetCountedUsersRating(int movieId)
        {
            try
            {
                await using var dbSqlConnection = new SqlConnection(_keys.DBSKEY);
                const string query = @"SELECT Count(UserId) FROM MovieRating WHERE MovieId= @movieId";
                var usersCounted = await dbSqlConnection.QuerySingleOrDefaultAsync<int?>(query, new {movieId});
                return usersCounted ?? 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}