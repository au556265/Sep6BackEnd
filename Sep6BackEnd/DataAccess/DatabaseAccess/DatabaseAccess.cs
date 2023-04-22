using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Sep6BackEnd.Controllers;

namespace Sep6BackEnd.DataAccess.DatabaseAccess
{
    public class DatabaseAccess : IDatabaseAccess
    {
        public User CreateUser(string userName, string email, string password)
        {
            using (var dbSqlConnection = new SqlConnection(Keys._DBSKEY))
            {
                const string query =
                    @"INSERT INTO Users (Username, Password, Email) OUTPUT INSERTED.* VALUES (@userName, @password, @email)";

                var output = dbSqlConnection.QuerySingle<User>(query, new {userName, password, email});

                return output;
            }
        }

        public User Login(string userName, string password)
        {
            using (var dbSqlConnection = new SqlConnection(Keys._DBSKEY))
            {
                const string query = @"SELECT 1 FROM Users WHERE Username= @userName AND Password= @password ";
                var results = (User) dbSqlConnection.QueryFirstOrDefault<User>(query, new {userName, password});
                return results;
            }
        }

        public void SetFavoriteMovie(string userName, string movieTitle)
        {
            using (var dbSqlConnection = new SqlConnection(Keys._DBSKEY))
            {
                const string query = @"INSERT INTO MovieFavorites (Username, Moviename) VALUES (@userName, @movieTitle) ";
                dbSqlConnection.Query(query, new {userName, movieTitle});
            }
        }

        public void SetMovieRating(string userName, string movieTitle, int rating)
        {
            using (var dbSqlConnection = new SqlConnection(Keys._DBSKEY))
            {
                const string query = @"BEGIN TRAN
IF EXISTS (select * from RatedMovies where Username = @userName AND Moviename = @movieTitle)
begin
   update RatedMovies set Rating = @rating
   where Username = @userName AND Moviename = @movieTitle
end
else
begin
   insert into RatedMovies (Username, Moviename, Rating)
   values (@userName, @movieTitle, @rating)
end
commit tran";
                dbSqlConnection.Query(query, new {userName, movieTitle, rating});
            }
        }

        public int GetMovieRating(string userName, string movieTitle)
        {
            using (var dbSqlConnection = new SqlConnection(Keys._DBSKEY))
            {
                const string query = @"SELECT Rating FROM RatedMovies WHERE Username= @userName AND Moviename= @movieTitle";
                var rating = dbSqlConnection.Query<int>(query, new {userName, movieTitle}).First();
                return rating;
            }
        }
    }
}