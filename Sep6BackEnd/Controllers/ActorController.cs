using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.BusinessLogic;

namespace Sep6BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorController : ControllerBase
    {
        private TmdbBL _tmdbBl;

        public ActorController()
        {
            _tmdbBl = new TmdbBL();
        }
        
        [HttpGet]
        [Route("getActors/{name}")]
        public  List<Actor> GetActors( [FromRoute] string name)
        {
            var results = _tmdbBl.GetTop10ActorsByName(name);
            return results;
        }
        
        
        [HttpGet]
        [Route("getMoviesByActors/{name}")]
        public  List<Cast> GetMoviesByActor( [FromRoute] string name)
        {
            var results = _tmdbBl.GetMoviesByActors(name);
            return results;
        }
        
        [HttpGet]
        [Route("getMostPopularActors")]
        public List<Actor> GetMostPopularActor()
        {
            var results = _tmdbBl.GetMostPopularActors();
            return results;
        }
      
    }
}