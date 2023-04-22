using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.BusinessLogic;

namespace Sep6BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeriesController : ControllerBase
    {
        private TmdbBL _tmdbBl;
        
        public SeriesController()
        {
            _tmdbBl = new TmdbBL();
        }
        
        [HttpGet]
        [Route("getMostPopularSeries")]
        public List<Series> GetMostPopularSeries()
        {
            var results = _tmdbBl.GetMostPopularSeries();
            return results;
        }
        
    }
}