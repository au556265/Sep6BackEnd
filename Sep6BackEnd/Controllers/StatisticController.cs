using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.BusinessLogic.Logic;
using Sep6BackEnd.DataAccess.TMDBAccess;

namespace Sep6BackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class StatisticController : ControllerBase
{
    private readonly IStatisticHandler _statisticHandler;
        
    public StatisticController(StatisticHandler statisticHandler)
    {
        _statisticHandler = statisticHandler;
    }
    [HttpGet]
    [Route("getAverageRatingTotal/{movieId}")]
    public async Task<ActionResult<double>> GetAverageRatingTotal(int movieId)
    {
        try
        {
            var results = await _statisticHandler.GetAverageRatingTotal(movieId);
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
    [Route("getAverageRatingForActorsMovie/{actorId}")]
    public async Task<ActionResult<double>> GetAverageRatingForActorsMovie(int actorId)
    {
        try
        {
            var results = await _statisticHandler.GetAverageRatingForActorsMovie(actorId);
            return Ok(results);
        }
        catch (TmdbException t)
        {
            return BadRequest("Error from tmdb with error with statuscode: "+ t.Message);
        }
        catch (Exception e)
        {
            if (e.Message.Equals("Not found"))
            {
                return NotFound($"Actor with {actorId} is not found");
            }

            return BadRequest(e.Message);
        }
    }
}