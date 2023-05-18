using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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

        public User CreateUser(string userName, string email, string password)
        {
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query =
                    @"INSERT INTO Users (Username, Password, Email) OUTPUT INSERTED.* VALUES (@userName, @password, @email)";

                var output = dbSqlConnection.QuerySingle<User>(query, new {userName, password, email});

                return output;
            }
        }

        public User Login(string userName, string password)
        {
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query = @"SELECT 1 FROM Users WHERE Username= @userName AND Password= @password ";
                var results = (User) dbSqlConnection.QueryFirstOrDefault<User>(query, new {userName, password});
                return results;
            }
        }

        public RatingObject SetFavoriteMovie(RatingObject ratingObject)
        {
            string userName = ratingObject.Username;
            int movieId = ratingObject.MovieId;
            int favorit = ratingObject.Favorit;
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query = @"BEGIN TRAN
IF EXISTS (select * from MovieFavorites2 where Username = @userName AND MovieId = @movieId)
begin
   update MovieFavorites2 set Favorit = @favorit
   where Username = @userName AND MovieId = @movieId
end
else
begin
   insert into MovieFavorites2 (Username, MovieId, Favorit)
   values (@userName, @movieId, @favorit)
end
commit tran";
                dbSqlConnection.Query(query, new {userName, movieId, favorit});
            }

            return new RatingObject();
        }
        
        

        public void SetFavoriteMovie(string userName, string movieTitle)
        {
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query = @"INSERT INTO MovieFavorites (Username, Moviename) VALUES (@userName, @movieTitle) ";
                dbSqlConnection.Query(query, new {userName, movieTitle});
            }
        }

        public RatingObject SetMovieRating( RatingObject ratingObject)
        {
            string userName = ratingObject.Username;
            int movieId = ratingObject.MovieId;
            int rating = ratingObject.Rating;
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query = @"BEGIN TRAN
IF EXISTS (select * from RatedMovies2 where Username = @userName AND MovieId = @movieId)
begin
   update RatedMovies2 set Rating = @rating
   where Username = @userName AND MovieId = @movieId
end
else
begin
   insert into RatedMovies2 (Username, MovieId, Rating)
   values (@userName, @movieId, @rating)
end
commit tran";
                dbSqlConnection.Query(query, new {userName, movieId, rating});
            }

            return new RatingObject();
        }

        public int GetMovieRating(string userName, int movieId)
        {
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query = @"SELECT Rating FROM RatedMovies2 WHERE Username= @userName AND MovieId= @movieId";
                var rating = dbSqlConnection.QueryFirstOrDefault<int>(query, new {userName, movieId});
                
                return rating;
            }
        }

        public bool GetFavoriteMovie(string userName, int movieId)
        {
            using (var dbSqlConnection = new SqlConnection(keys.DBSKEY))
            {
                const string query = @"SELECT Favorit FROM MovieFavorites2 WHERE Username= @userName AND MovieId= @movieId";
                var favorite = dbSqlConnection.QueryFirstOrDefault<bool>(query, new {userName, movieId});
                
                return favorite;
            }
        }

      
        public async Task <IEnumerable<int>>GetAllMyFavoritesIds(string userName)
        {
            using var dbSqlConnection = new SqlConnection(keys.DBSKEY);
            
            const string query = @"SELECT MovieId FROM MovieFavorites2 WHERE Username= @userName";
            var allMyFavorites =  await dbSqlConnection.QueryAsync<int>(query, new {userName});
            return allMyFavorites;
            
        }
        
    }
}