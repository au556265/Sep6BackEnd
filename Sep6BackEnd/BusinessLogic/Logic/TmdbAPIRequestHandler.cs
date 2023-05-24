using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sep6BackEnd.Controllers;
using Sep6BackEnd.DataAccess.IMDBAccess;
using Sep6BackEnd.DataAccess.TMDBAccess;

namespace Sep6BackEnd.BusinessLogic
{
    public class TmdbAPIRequestHandler
    {
        private ITmdbAccess _tmdbAccess;

        public TmdbAPIRequestHandler(TmdbAccess tmdbAccess)
        {
            _tmdbAccess = tmdbAccess;
        }


        public async Task<List<Movie>> GetTop20MoviesByTitle(string name)
        {
            var allMovies = await _tmdbAccess.GetMovieByTitle(name);
            return allMovies.Take(20).ToList();
            
        }
        
        public async Task<List<Actor>> GetTop20ActorsByName(string name)
        {
            var allActors = await _tmdbAccess.GetByActorByName(name);
            return allActors.Take(20).ToList();
        }

        public async Task<PersonDetails> GetActorById(int id)
        {
            var actor = await _tmdbAccess.GetActorById(id);
            return actor;
        }

        public async Task<List<MoviesByActor>> GetMoviesByActor(string name)
        {
            var allMoviesByActor = await _tmdbAccess.GetMoviesByActor(name);
            
            return allMoviesByActor;
        }
        
        public async Task<List<MoviesByActor>> GetMoviesByActorId(int id)
        {
            var allMoviesByActor = await _tmdbAccess.GetMoviesByActorId(id);
            
            return allMoviesByActor;
        }

        public async Task<List<Series>> GetMostPopularSeries()
        {
            var weeklyPopularSeries = await _tmdbAccess.GetMostPopularSeries();
            return weeklyPopularSeries;
        }
        
        public async Task<List<Movie>> GetMostPopularMovies()
        {
            return await _tmdbAccess.GetMostPopularMovies();
        }
        
        public async Task<List<Actor>> GetMostPopularActors()
        {
            var weeklyPopularActors = await _tmdbAccess.GetMostPopularActors();
            return weeklyPopularActors;
        }

        public async Task<Movie> GetMovie(int id)
        {
            return await _tmdbAccess.GetMovie(id);
        }

        public async Task<List<Cast>> GetActorsByMovie(int id)
        {
            return await _tmdbAccess.GetActorByMovie(id);
        }
    }
}