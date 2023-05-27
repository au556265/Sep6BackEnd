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
    public class SeriesController : ControllerBase
    {
        private readonly ITmdbApiRequestHandler _tmdbApiRequestHandler;
        
        public SeriesController(TmdbApiRequestHandler tmdbApiRequestHandler)
        {
            _tmdbApiRequestHandler = tmdbApiRequestHandler;
        }
        
        [HttpGet]
        [Route("getWeeklyTrendingSeries")]
        public async Task<ActionResult<List<Series>>> GetWeeklyTrendingSeries()
        {
            try
            {
                var results = await _tmdbApiRequestHandler.GetWeeklyTrendingSeries();
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