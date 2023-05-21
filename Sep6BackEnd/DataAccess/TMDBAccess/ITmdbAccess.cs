using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6BackEnd.Controllers;

namespace Sep6BackEnd.DataAccess.IMDBAccess
{
    public interface ITmdbAccess
    {
        Task<List<Movie>> GetByTitle(string name);
        Task<List<Actor>> GetByActorByName(string name);
        Task<PersonDetails> GetActorById(int id);
        Task<List<MoviesByActor>> GetMoviesByActor(string name);

        Task<List<Series>> GetMostPopularSeries();

        Task<List<Movie>> GetMostPopularMovies();

        Task<List<Actor>> GetMostPopularActors();
        Task<Movie> GetMovie(int id);
        Task<List<Cast>> GetActorByMovie(int movieId);
        
    }
}