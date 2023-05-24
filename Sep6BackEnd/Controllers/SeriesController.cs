using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<List<Series>> GetMostPopularSeries()
        {
            var results = await _tmdbApiRequestHandler.GetMostPopularSeries();
            return results;
        }
        
    }
}