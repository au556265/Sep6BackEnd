using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sep6BackEnd.BusinessLogic;

namespace Sep6BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class MovieController: ControllerBase
    {
        private TmdbAPIRequestHandler _tmdbApiRequestHandler;
        
        public MovieController(TmdbAPIRequestHandler tmdbApiRequestHandler)
        {
            _tmdbApiRequestHandler=tmdbApiRequestHandler;
        }
        
        [HttpGet]
        [Route("get20MoviesBySearch/{name}")]
        public async Task<List<Movie>> Get20MoviesBySearch( [FromRoute] string name)
        {
            var results = await _tmdbApiRequestHandler.GetTop20MoviesByTitle(name);
            return results;
        }
        [HttpGet]
        [Route("getMovie/{id}")]
        public async Task<Movie> GetMovie( [FromRoute] int id)
        {
            return await _tmdbApiRequestHandler.GetMovie(id);
        }
        [HttpGet]
        [Route("getMostPopularMovies")]
        public async Task<List<Movie>> GetMostPopularMovies()
        {
            return await _tmdbApiRequestHandler.GetMostPopularMovies();
           
        }

        [HttpGet]
        [Route("getActorsByMovie/{id}")]
        public async Task<List<Cast>> GetActorsByMovie(int id)
        {
            return await _tmdbApiRequestHandler.GetActorsByMovie(id);
        }


    }
}