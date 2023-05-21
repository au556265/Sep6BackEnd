using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.BusinessLogic;

namespace Sep6BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeriesController : ControllerBase
    {
        private TmdbAPIRequestHandler _tmdbApiRequestHandler;
        
        public SeriesController(TmdbAPIRequestHandler tmdbApiRequestHandler)
        {
            _tmdbApiRequestHandler = tmdbApiRequestHandler;
        }
        
        [HttpGet]
        [Route("getMostPopularSeries")]
        public List<Series> GetMostPopularSeries()
        {
            var results = _tmdbApiRequestHandler.GetMostPopularSeries();
            return results;
        }
        
    }
}