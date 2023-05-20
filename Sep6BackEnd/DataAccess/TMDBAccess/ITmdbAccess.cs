using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6BackEnd.Controllers;

namespace Sep6BackEnd.DataAccess.IMDBAccess
{
    public interface ITmdbAccess
    {
        public Task<List<Movie>> getByTitle(string name);
        public Task<List<Actor>> getByActorByName(string name);
        public Task<List<MoviesByActor>> getMoviesByActor(string name);

        public Task<List<Series>> getMostPopularSeries();

        public Task<List<Movie>> getMostPopularMovies();

        public Task<List<Actor>> getMostPopularActors();
        public Task<Movie> getMovie(int id);
        public Task<List<Cast>> getActorByMovie(int movieId);
        
    }
}