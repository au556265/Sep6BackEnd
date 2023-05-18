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
            this._tmdbApiRequestHandler=tmdbApiRequestHandler;
        }
        
        [HttpGet]
        [Route("get5MoviesBySearch/{name}")]
        public  List<Movie> get5MoviesBySearch( [FromRoute] string name)
        {
            var results = _tmdbApiRequestHandler.GetTop5MoviesByTitle(name);
            return results;
        }
        [HttpGet]
        [Route("getMovie/{id}")]
        public  Movie GetMovie( [FromRoute] int id)
        {
            var results = _tmdbApiRequestHandler.GetMovie(id);
            return results;
        }
        [HttpGet]
        [Route("getMostPopularMovies")]
        public List<Movie> GetMostPopularMovies()
        {
            var results = _tmdbApiRequestHandler.GetMostPopularMovies();
            return results;
        }

        [HttpGet]
        [Route("getActorsByMovie/{id}")]
        public List<Cast> GetActorsByMovie(int id)
        {
            var results = _tmdbApiRequestHandler.GetActorsByMovie(id);
            return results;
        }


    }
}