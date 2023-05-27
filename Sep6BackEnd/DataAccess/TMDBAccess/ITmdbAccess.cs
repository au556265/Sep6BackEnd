using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6BackEnd.DataAccess.DomainClasses.APIModels;

namespace Sep6BackEnd.DataAccess.TMDBAccess
{
    public interface ITmdbAccess
    {
        Task<List<Movie>> GetMovieByTitle(string name);
        Task<List<Actor>> GetByActorByName(string name);
        Task<PersonDetails> GetActorById(int id);
        Task<List<MoviesByActor>> GetMoviesByActor(string name);
        Task<List<MoviesByActor>> GetMoviesByActorId(int id);
        Task<List<Series>> GetWeeklyTrendingSeries();
        Task<List<Movie>> GetMostPopularMovies();
        Task<List<Movie>> GetUpcomingMovies();
        Task<List<Movie>> GetTopRatedMovies();
        Task<List<Movie>> GetWeeklyTrendingMovies();
        Task<List<Actor>> GetMostPopularActors();
        Task<List<Actor>> GetWeeklyTrendingActors();
        Task<Movie> GetMovie(int id);
        Task<List<Cast>> GetActorByMovieId(int movieId);

    }
}