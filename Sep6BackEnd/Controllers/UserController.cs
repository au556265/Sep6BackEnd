using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.BusinessLogic.Logic;
using Sep6BackEnd.DataAccess.DomainClasses.APIModels;
using Sep6BackEnd.DataAccess.DomainClasses.DatabaseModels;

namespace Sep6BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsersRequestHandler _usersRequestHandler;
        
        public UserController(UsersRequestHandler usersRequestHandler)
        {
            _usersRequestHandler = usersRequestHandler;
        }
        
        [HttpPost]
        [Route("postCreateUser/{userName}/{email}/{password}")]
        public async Task<ActionResult<Users>> CreateUser([FromRoute] string userName, string email, string password)
        {
            try
            {
                var results = await _usersRequestHandler.CreateUser(userName, email, password);
                Console.WriteLine(results.GetType());
                
                return Ok(results);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured in: " + e.Message );
                return BadRequest( $"{email} or {userName} is already taken please select something else");
            }
        }
        
        //Try Catch not needed
        [HttpGet]
        [Route("login/{userName}/{password}")]
        public async Task<ActionResult<Users>> Login([FromRoute] string userName, string password)
        {
            try
            {
                var results =await  _usersRequestHandler.Login(userName, password);
               if (results == null)
                {
                    return NotFound("Username or password is wrong");
                }
                
                return Ok(results);
            }
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            }
        }
        
        // TODO handle when Ok and bad
        // controller 
        [HttpGet]
        [Route("getFavoriteMovie/{userId}/{movieId}")]
        public async Task<ActionResult<bool>> GetFavoriteMovie([FromRoute] int userId, int movieId)
        {
            try
            {
                var result = await _usersRequestHandler.GetFavoriteMovie(userId, movieId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Exception occured in: " + e.Message);
            }
        }
        
        // TODO handle when Ok and bad
        [HttpPost]
        [Route("setFavoriteMovie")]
        public async Task<ActionResult<MovieFavorite>> SetFavoriteMovie([FromBody] MovieFavorite movieFavorite)
        {
            try
            {
                var result = await _usersRequestHandler.SetFavoriteMovie(movieFavorite);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // TODO handle when Ok and bad
        [HttpPost]
        [Route("setMovieRating")]
        public async Task<ActionResult<MovieRating>> SetMovieRating([FromBody] MovieRating movieRating)
        {
            try
            {
                var result = await _usersRequestHandler.SetMovieRating(movieRating);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // TODO handle when Ok and bad
        [HttpGet]
        [Route("getMovieRating/{userId}/{movieId}")]
        public async Task<ActionResult<int>> GetMovieRating([FromRoute] int userId, int movieId)
        {
            try
            {
                var rating = await _usersRequestHandler.GetMovieRating(userId,movieId);
                return Ok(rating);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // TODO handle when Ok and bad
        [HttpGet]
        [Route("getAllMyFavoriteMovies/{userId}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMyFavoritesMovies(int userId)
        {
            try
            {
                var results = await _usersRequestHandler.GetAllMyFavoritesMovies(userId);
                return Ok(results);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}