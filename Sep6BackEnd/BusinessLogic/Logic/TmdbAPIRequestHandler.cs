using System.Collections.Generic;
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
            var allMovies = _tmdbAccess.getByTitle(name).Result;

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
            var allActors = _tmdbAccess.getByActorByName(name).Result;

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
        
        public List<MoviesByActor> GetMoviesByActor(string name)
        {
            var allMoviesByActor = _tmdbAccess.getMoviesByActor(name).Result;
            
            return allMoviesByActor;
        }

        public List<Series> GetMostPopularSeries()
        {
            var weeklyPopularSeries = _tmdbAccess.getMostPopularSeries().Result;
            return weeklyPopularSeries;
        }
        
        public List<Movie> GetMostPopularMovies()
        {
            var weeklyPopularMovies = _tmdbAccess.getMostPopularMovies().Result;
            return weeklyPopularMovies;
        }
        
        public List<Actor> GetMostPopularActors()
        {
            var weeklyPopularActors = _tmdbAccess.getMostPopularActors().Result;
            return weeklyPopularActors;
        }

        public Movie GetMovie(int id)
        {
            return _tmdbAccess.getMovie(id).Result;
        }

        public List<Cast> GetActorsByMovie(int id)
        {
            var allCast = _tmdbAccess.getActorByMovie(id).Result;
            return allCast;
        }
    }
}