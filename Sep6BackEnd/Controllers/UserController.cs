using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Sep6BackEnd.BusinessLogic;

namespace Sep6BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUsersBL _usersBl;
        
        public UserController()
        {
            _usersBl = new UsersBL();
        }
        
        [HttpPost]
        [Route("postCreateUser/{userName}/{email}/{password}")]
        public User CreateUser([FromRoute] string userName, string email, string password)
        {
            var results = _usersBl.CreateUser(userName, email, password);
            return results;
        }
        
        [HttpGet]
        [Route("login/{userName}/{password}")]
        public string Login([FromRoute] string userName, string password)
        {
            var results = _usersBl.Login(userName, password);
            return results;
        }
        
        [HttpGet]
        [Route("getFavoriteMovie/{userName}/{movieId}")]
        public bool getFavoriteMovie([FromRoute] string userName, int movieId)
        {
            return _usersBl.GetFavoriteMovie(userName, movieId);
        }
        
        [HttpPost]
        [Route("setFavoriteMovie")]
        public RatingObject SetFavoriteMovie([FromBody] RatingObject ratingObject)
        {
            return _usersBl.SetFavoriteMovie(ratingObject);
        }
        
        [HttpPost]
        [Route("setMovieRating")]
        public RatingObject SetMovieRating([FromBody] RatingObject ratingObject)
        {
            return _usersBl.SetMovieRating(ratingObject);
        }
        
        [HttpGet]
        [Route("getMovieRating/{userName}/{movieId}")]
        public int GetMovieRating([FromRoute] string userName, int movieId)
        {
           var rating = _usersBl.GetMovieRating(userName,movieId);
           return rating;
        }
    }
}