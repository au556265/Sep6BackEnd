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
            this._usersRequestHandler = usersRequestHandler;
        }
        
        [HttpPost]
        [Route("postCreateUser/{userName}/{email}/{password}")]
        public User CreateUser([FromRoute] string userName, string email, string password)
        {
            var results = _usersRequestHandler.CreateUser(userName, email, password);
            return results;
        }
        
        [HttpGet]
        [Route("login/{userName}/{password}")]
        public string Login([FromRoute] string userName, string password)
        {
            var results = _usersRequestHandler.Login(userName, password);
            return results;
        }
        
        [HttpGet]
        [Route("getFavoriteMovie/{userName}/{movieId}")]
        public bool getFavoriteMovie([FromRoute] string userName, int movieId)
        {
            return _usersRequestHandler.GetFavoriteMovie(userName, movieId);
        }
        
        [HttpPost]
        [Route("setFavoriteMovie")]
        public RatingObject SetFavoriteMovie([FromBody] RatingObject ratingObject)
        {
            return _usersRequestHandler.SetFavoriteMovie(ratingObject);
        }
        
        [HttpPost]
        [Route("setMovieRating")]
        public RatingObject SetMovieRating([FromBody] RatingObject ratingObject)
        {
            return _usersRequestHandler.SetMovieRating(ratingObject);
        }
        
        [HttpGet]
        [Route("getMovieRating/{userName}/{movieId}")]
        public int GetMovieRating([FromRoute] string userName, int movieId)
        {
           var rating = _usersRequestHandler.GetMovieRating(userName,movieId);
           return rating;
        }
        
        [HttpGet]
        [Route("getAllMyFavoriteMovies/{userName}")]
        public async Task<IEnumerable<Movie>> GetAllMyFavoritesMovies(string userName)
        {
            var results = await _usersRequestHandler.GetAllMyFavoritesMovies(userName);
            return results;
        }
    }
}