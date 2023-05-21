using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sep6BackEnd.DataAccess.DatabaseAccess;
using Sep6BackEnd.DataAccess.IMDBAccess;
using Sep6BackEnd.DataAccess.TMDBAccess;

namespace Sep6BackEnd.BusinessLogic;

public class StatisticHandler : IStatisticHandler
{
    private readonly IDatabaseAccess _databaseAccess;
    private readonly ITmdbAccess _tmdbAccess;

    public StatisticHandler(DatabaseAccess databaseAccess, TmdbAccess tmdbAccess)
    {
        _databaseAccess = databaseAccess;
        _tmdbAccess = tmdbAccess;
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

    public async Task<double> GetAverageRatingForActorsMovie(int actorId)
    {
        var allMoviesByActor = await _tmdbAccess.GetMoviesByActorId(actorId);
        List<double> sumOfRatingsDb = new List<double>();
        List<int> sumOfCountDb = new List<int>();
        List<double> sumOfRatingsTmdb = new List<double>();
        List<int> sumOfCountTmdb = new List<int>();
        
        foreach (var vMoviesByActor in allMoviesByActor)
        {
            
            var ratingSumFromUsers= await _databaseAccess.GetRatingSumFromUsers(vMoviesByActor.id);
            sumOfRatingsDb.Add(ratingSumFromUsers);
            var countedUsersRating = await _databaseAccess.GetCountedUsersRating(vMoviesByActor.id);
            sumOfCountDb.Add(countedUsersRating);
            
            var tmdbRatingCount = vMoviesByActor.vote_count;
            sumOfCountTmdb.Add(tmdbRatingCount);
            
            var tmdbRatingAverage = vMoviesByActor.vote_average;
            var tmdbRatingSum = tmdbRatingAverage * tmdbRatingCount;
            sumOfRatingsTmdb.Add(tmdbRatingSum);

        }

        var totalRatingAverage = (sumOfRatingsTmdb.Sum()+sumOfRatingsDb.Sum())/(sumOfCountTmdb.Sum()+sumOfCountDb.Sum());
        return totalRatingAverage;
    }
}