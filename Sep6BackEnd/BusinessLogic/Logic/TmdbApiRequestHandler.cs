using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sep6BackEnd.DataAccess.DomainClasses.APIModels;
using Sep6BackEnd.DataAccess.TMDBAccess;

namespace Sep6BackEnd.BusinessLogic.Logic
{
    public class TmdbApiRequestHandler : ITmdbApiRequestHandler
    {
        private readonly ITmdbAccess _tmdbAccess;
        public TmdbApiRequestHandler(TmdbAccess tmdbAccess)
        {
            _tmdbAccess = tmdbAccess;
        }

        public async Task<List<Movie>> GetTop20MoviesByTitle(string name)
        {
            try
            {
                var allMovies = await _tmdbAccess.GetMovieByTitle(name);
                return allMovies.Take(20).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        public async Task<List<Actor>> GetTop20ActorsByName(string name)
        {
            try
            {
                var allActors = await _tmdbAccess.GetByActorByName(name);
                return allActors.Take(20).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<PersonDetails> GetActorById(int id)
        {
            try
            {
                var actor = await _tmdbAccess.GetActorById(id);
                return actor;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<MoviesByActor>> GetMoviesByActor(string name)
        {
            try
            {
                var allMoviesByActor = await _tmdbAccess.GetMoviesByActor(name);
                return allMoviesByActor;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<List<MoviesByActor>> GetMoviesByActorId(int id)
        {
            try
            {
                var allMoviesByActor = await _tmdbAccess.GetMoviesByActorId(id);
                return allMoviesByActor;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Series>> GetMostPopularSeries()
        {
            try
            {
                var weeklyPopularSeries = await _tmdbAccess.GetMostPopularSeries();
                return weeklyPopularSeries;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<List<Movie>> GetMostPopularMovies()
        {
            try
            {
                return await _tmdbAccess.GetMostPopularMovies();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Movie>> GetUpcomingMovies()
        {
            try
            {
                return await _tmdbAccess.GetUpcomingMovies();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<List<Movie>> GetTopRatedMovies()
        {
            try
            {
                return await _tmdbAccess.GetTopRatedMovies();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Actor>> GetMostPopularActors()
        {
            try
            {
                var weeklyPopularActors = await _tmdbAccess.GetMostPopularActors();
                return weeklyPopularActors;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Movie> GetMovie(int id)
        {
            try
            {
                return await _tmdbAccess.GetMovie(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<List<Cast>> GetActorsByMovieId(int id)
        {
            try
            {
                return await _tmdbAccess.GetActorByMovieId(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}