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
        
        [HttpPost]
        [Route("setFavoriteMovie/{userName}/{movieTitle}")]
        public void SetFavoriteMovie([FromRoute] string userName, string movieTitle)
        {
            _usersBl.SetFavoriteMovie(userName, movieTitle);
        }
        
        [HttpPost]
        [Route("setMovieRating/{userName}/{movieTitle}/{rating}")]
        public void SetMovieRating([FromRoute] string userName, string movieTitle, int rating)
        {
            _usersBl.SetMovieRating(userName,movieTitle,rating);
        }
        
        [HttpGet]
        [Route("getMovieRating/{userName}/{movieTitle}")]
        public int GetMovieRating([FromRoute] string userName, string movieTitle)
        {
           var rating = _usersBl.GetMovieRating(userName,movieTitle);
           return rating;
        }
    }
}