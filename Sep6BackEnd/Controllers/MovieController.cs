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
        private TmdbBL _tmdbBl;
        
        public MovieController()
        {
            _tmdbBl = new TmdbBL();
        }
        
        [HttpGet]
        [Route("get5MoviesBySearch/{name}")]
        public  List<Movie> get5MoviesBySearch( [FromRoute] string name)
        {
            var results = _tmdbBl.GetTop5MoviesByTitle(name);
            return results;
        }
        [HttpGet]
        [Route("getMovie/{id}")]
        public  Movie GetMovie( [FromRoute] int id)
        {
            var results = _tmdbBl.GetMovie(id);
            return results;
        }
        [HttpGet]
        [Route("getMostPopularMovies")]
        public List<Movie> GetMostPopularMovies()
        {
            var results = _tmdbBl.GetMostPopularMovies();
            return results;
        }
        
    }
}