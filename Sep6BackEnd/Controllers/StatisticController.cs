using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.BusinessLogic;

namespace Sep6BackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class StatisticController : ControllerBase
{
    private IStatisticHandler _statisticHandler;
        
    public StatisticController(StatisticHandler statisticHandler)
    {
        _statisticHandler = statisticHandler;
    }
    [HttpGet]
    [Route("getAverageRatingTotal/{movieId}")]
    public async Task<double> GetAverageRatingTotal(int movieId)
    {
        var results = await _statisticHandler.GetAverageRatingTotal(movieId);
        return results;
    }
    
    [HttpGet]
    [Route("getAverageRatingForActorsMovie/{actorId}")]
    public async Task<double> GetAverageRatingForActorsMovie(int actorId)
    {
        var results = await _statisticHandler.GetAverageRatingForActorsMovie(actorId);
        return results;
    }
}