using System.Collections.Generic;
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


        public List<Movie> GetTop5MoviesByTitle(string name)
        {
            var allMovies = _tmdbAccess.GetByTitle(name).Result;

            int i = 0;
            List<Movie> returnList = new List<Movie>();
            foreach (var movie in allMovies)
            {
                returnList.Add(movie);
                i++;

                if (i == 5)
                    break;
            }

            return returnList;
        }
        
        public List<Actor> GetTop10ActorsByName(string name)
        {
            var allActors = _tmdbAccess.GetByActorByName(name).Result;

            int i = 0;
            List<Actor> returnList = new List<Actor>();
            foreach (var actor in allActors)
            {
                returnList.Add(actor);
                i++;

                if (i == 10)
                    break;
            }

            return returnList;
        }

        public async Task<PersonDetails> GetActorById(int id)
        {
            var actor = await _tmdbAccess.GetActorById(id);
            return actor;
        }

        public List<MoviesByActor> GetMoviesByActor(string name)
        {
            var allMoviesByActor = _tmdbAccess.GetMoviesByActor(name).Result;
            
            return allMoviesByActor;
        }
        
        public async Task<List<MoviesByActor>> GetMoviesByActorId(int id)
        {
            var allMoviesByActor = await _tmdbAccess.GetMoviesByActorId(id);
            
            return allMoviesByActor;
        }

        public List<Series> GetMostPopularSeries()
        {
            var weeklyPopularSeries = _tmdbAccess.GetMostPopularSeries().Result;
            return weeklyPopularSeries;
        }
        
        public List<Movie> GetMostPopularMovies()
        {
            var weeklyPopularMovies = _tmdbAccess.GetMostPopularMovies().Result;
            return weeklyPopularMovies;
        }
        
        public List<Actor> GetMostPopularActors()
        {
            var weeklyPopularActors = _tmdbAccess.GetMostPopularActors().Result;
            return weeklyPopularActors;
        }

        public Movie GetMovie(int id)
        {
            return _tmdbAccess.GetMovie(id).Result;
        }

        public List<Cast> GetActorsByMovie(int id)
        {
            var allCast = _tmdbAccess.GetActorByMovie(id).Result;
            return allCast;
        }
    }
}