﻿using System.Collections.Generic;
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
            _tmdbApiRequestHandler =tmdbApiRequestHandler;
        }
        
        [HttpGet]
        [Route("getActors/{name}")]
        public async Task<List<Actor>> GetActors( [FromRoute] string name)
        {
            
            return await _tmdbApiRequestHandler.GetTop20ActorsByName(name);
        }
        
        [HttpGet]
        [Route("getActorById/{id}")]
        public async Task<PersonDetails>GetActorById( [FromRoute] int id)
        {
            var result = await _tmdbApiRequestHandler.GetActorById(id);
            return result;
        }
        
        [HttpGet]
        [Route("getMoviesByActor/{name}")]
        public async Task<ActionResult<List<MoviesByActor>>> GetMoviesByActor( [FromRoute] string name)
        {
            var results = await _tmdbApiRequestHandler.GetMoviesByActor(name);
            if (results.Count == 0)
            {
                return NotFound("Actor can't be found");
            }

            return Ok(results);
        }
        
        [HttpGet]
        [Route("getMoviesByActorId/{id}")]
        public async Task<ActionResult<List<MoviesByActor>>> GetMoviesByActorId( [FromRoute] int id)
        {
            var results = await _tmdbApiRequestHandler.GetMoviesByActorId(id);
            if (results.Count == 0)
            {
                return NotFound("Actor can't be found");
            }

            return Ok(results);
        }
        
        [HttpGet]
        [Route("getMostPopularActors")]
        public async Task<List<Actor>> GetMostPopularActor()
        {
            var results = await _tmdbApiRequestHandler.GetMostPopularActors();
            return results;
        }
      
    }
}