using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.BusinessLogic;

namespace Sep6BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorController : ControllerBase
    {
        private TmdbAPIRequestHandler _tmdbApiRequestHandler;

        public ActorController(TmdbAPIRequestHandler tmdbApiRequestHandler)
        {
            this._tmdbApiRequestHandler =tmdbApiRequestHandler;
        }
        
        [HttpGet]
        [Route("getActors/{name}")]
        public  List<Actor> GetActors( [FromRoute] string name)
        {
            var results = _tmdbApiRequestHandler.GetTop10ActorsByName(name);
            return results;
        }
        
        [HttpGet]
        [Route("getActorById/{id}")]
        public async Task<PersonDetails>GetActorById( [FromRoute] int id)
        {
            var result = await _tmdbApiRequestHandler.GetActorById(id);
            return result;
        }
        
        [HttpGet]
        [Route("getMoviesByActors/{name}")]
        public  ActionResult<List<MoviesByActor>> GetMoviesByActor( [FromRoute] string name)
        {
            var results = _tmdbApiRequestHandler.GetMoviesByActor(name);
            if (results.Count == 0)
            {
                return NotFound("Actor can't be found");
            }

            return Ok(results);
        }
        
        [HttpGet]
        [Route("getMostPopularActors")]
        public List<Actor> GetMostPopularActor()
        {
            var results = _tmdbApiRequestHandler.GetMostPopularActors();
            return results;
        }
      
    }
}