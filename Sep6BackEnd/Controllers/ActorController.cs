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
    public class ActorController : ControllerBase
    {
        private readonly TmdbApiRequestHandler _tmdbApiRequestHandler;

        public ActorController(TmdbApiRequestHandler tmdbApiRequestHandler)
        {
            _tmdbApiRequestHandler =tmdbApiRequestHandler;
        }
        
        [HttpGet]
        [Route("getActors/{name}")]
        public async Task<ActionResult<List<Actor>>> GetActors( [FromRoute] string name)
        {
            try
            {
                var result = await _tmdbApiRequestHandler.GetTop20ActorsByName(name);
                if (result.Count == 0)
                {
                    return NotFound($"Actor with {name} does not exist");
                }
                return Ok(result);

            }
            catch (TmdbException t)
            {
                return BadRequest("Error from tmdb with error with statuscode: "+ t.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // TODO handle bad/ok
        [HttpGet]
        [Route("getActorById/{id}")]
        public async Task<ActionResult<PersonDetails>>GetActorById( [FromRoute] int id)
        {
            try
            {
                var result = await _tmdbApiRequestHandler.GetActorById(id);
               
                return Ok(result);

            }
            catch (TmdbException t)
            {
                return BadRequest("Error from tmdb with error with statuscode: "+ t.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        [Route("getMoviesByActor/{name}")]
        public async Task<ActionResult<List<MoviesByActor>>> GetMoviesByActor( [FromRoute] string name)
        {
            try
            {
                var results = await _tmdbApiRequestHandler.GetMoviesByActor(name);
                if (results.Count == 0)
                {
                    return NotFound($"Actor with {name} can not be found");
                }

                return Ok(results);
            }
            catch (TmdbException te)
            {
                return BadRequest("Exception occured in 3rd party TMDB: " + te.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        [Route("getMoviesByActorId/{id}")]
        public async Task<ActionResult<List<MoviesByActor>>> GetMoviesByActorId( [FromRoute] int id)
        {
            try
            {
                var results = await _tmdbApiRequestHandler.GetMoviesByActorId(id);
                if (results.Count == 0)
                {
                    return NotFound($"Actor with {id} can not be found");
                }

                return Ok(results);
            }
            catch (TmdbException t)
            {
                return BadRequest("Error from tmdb with error with statuscode: "+ t.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet]
        [Route("getMostPopularActors")]
        public async Task<ActionResult<List<Actor>>> GetMostPopularActor()
        {
            try
            {
                var results = await _tmdbApiRequestHandler.GetMostPopularActors();
                return Ok(results);
            }
            catch (TmdbException t)
            {
                return BadRequest("Error from tmdb with error with statuscode: "+ t.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
      
    }
}