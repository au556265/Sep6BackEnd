using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.BusinessLogic;

namespace Sep6BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUsersRequestHandler _usersRequestHandler;
        
        public UserController(UsersRequestHandler usersRequestHandler)
        {
            _usersRequestHandler = usersRequestHandler;
        }
        
        [HttpPost]
        [Route("postCreateUser/{userName}/{email}/{password}")]
        public async Task<ActionResult<Users>> CreateUser([FromRoute] string userName, string email, string password)
        { 
            var results = await _usersRequestHandler.CreateUser(userName, email, password);
            if (results == null)
            {
                return BadRequest("Email or username is already taken please select something else");
            }

            return results;

        }
        
        [HttpGet]
        [Route("login/{userName}/{password}")]
        public async Task<ActionResult<Users>> Login([FromRoute] string userName, string password)
        {
            var results =await  _usersRequestHandler.Login(userName, password);
            if (results == null)
            {
                return BadRequest("Username or password is wrong");
            }
            return results;
        }
        
        [HttpGet]
        [Route("getFavoriteMovie/{userId}/{movieId}")]
        public async Task<bool> getFavoriteMovie([FromRoute] int userId, int movieId)
        {
            return await _usersRequestHandler.GetFavoriteMovie(userId, movieId);
        }
        
        [HttpPost]
        [Route("setFavoriteMovie")]
        public async Task<MovieFavorite> SetFavoriteMovie([FromBody] MovieFavorite movieFavorite)
        {
            return await _usersRequestHandler.SetFavoriteMovie(movieFavorite);
        }
        
        [HttpPost]
        [Route("setMovieRating")]
        public async Task<MovieRating> SetMovieRating([FromBody] MovieRating movieRating)
        {
            return await _usersRequestHandler.SetMovieRating(movieRating);
        }
        
        [HttpGet]
        [Route("getMovieRating/{userId}/{movieId}")]
        public async Task<int> GetMovieRating([FromRoute] int userId, int movieId)
        {
           var rating = await _usersRequestHandler.GetMovieRating(userId,movieId);
           return rating;
        }
        
        [HttpGet]
        [Route("getAllMyFavoriteMovies/{userId}")]
        public async Task<IEnumerable<Movie>> GetAllMyFavoritesMovies(int userId)
        {
            var results = await _usersRequestHandler.GetAllMyFavoritesMovies(userId);
            return results;
        }
        
    }
}