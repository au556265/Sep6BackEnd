using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.BusinessLogic.Logic;
using Sep6BackEnd.DataAccess.DomainClasses.APIModels;
using Sep6BackEnd.DataAccess.TMDBAccess;

namespace Sep6BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController: ControllerBase
    {
        private readonly ITmdbApiRequestHandler _tmdbApiRequestHandler;
        
        public MovieController(TmdbApiRequestHandler tmdbApiRequestHandler)
        {
            _tmdbApiRequestHandler=tmdbApiRequestHandler;
        }
        
        [HttpGet]
        [Route("get20MoviesBySearch/{name}")]
        public async Task<ActionResult<List<Movie>>> Get20MoviesBySearch( [FromRoute] string name)
        {
            try
            {
                var results = await _tmdbApiRequestHandler.GetTop20MoviesByTitle(name);
                if (results.Count == 0)
                {
                    return NotFound($"The movie {name} can not be found");
                }
                return Ok(results);
            }
            catch (TmdbException t)
            {
                return BadRequest("Error from tmdb with error with statuscode: "+ t.Message); 
            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }
        }
        
        [HttpGet]
        [Route("getMovie/{id}")]
        public async Task<ActionResult<Movie>> GetMovie( [FromRoute] int id)
        {
            try
            {
                return Ok(await _tmdbApiRequestHandler.GetMovie(id));
            }
            catch (TmdbException t)
            {
                return BadRequest("Error from tmdb with error with statuscode: "+ t.Message); 
            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }
        }
        
        [HttpGet]
        [Route("getMostPopularMovies")]
        public async Task<ActionResult<List<Movie>>> GetMostPopularMovies()
        {
            try
            {
                return Ok(await _tmdbApiRequestHandler.GetMostPopularMovies());
            }
            catch (TmdbException t)
            {
                return BadRequest("Error from tmdb with error with statuscode: "+ t.Message); 
            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }
        }
        
        [HttpGet]
        [Route("getUpcomingMovies")]
        public async Task<ActionResult<List<Movie>>> GetUpComingMovies()
        {
            try
            {
                return Ok(await _tmdbApiRequestHandler.GetUpcomingMovies());
            }
            catch (TmdbException t)
            {
                return BadRequest("Error from tmdb with error with statuscode: "+ t.Message); 
            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }
        }
        
        [HttpGet]
        [Route("getTopRatedMovies")]
        public async Task<ActionResult<List<Movie>>> GetTopRatedMovies()
        {
            try
            {
                return Ok(await _tmdbApiRequestHandler.GetTopRatedMovies());
            }
            catch (TmdbException t)
            {
                return BadRequest("Error from tmdb with error with statuscode: "+ t.Message); 
            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }
        }
        
        [HttpGet]
        [Route("getActorsByMovie/{id}")]
        public async Task<ActionResult<List<Cast>>> GetActorsByMovieId(int id)
        {
            try
            {
                var result = await _tmdbApiRequestHandler.GetActorsByMovieId(id);
                return Ok(result);
            }
            catch (TmdbException t)
            {
                return BadRequest("Error from tmdb with error with statuscode: "+ t.Message); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}