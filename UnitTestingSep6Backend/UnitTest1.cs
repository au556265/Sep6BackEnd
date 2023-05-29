using System.Net;
using System.Text;
using Newtonsoft.Json;
using NSubstitute;
using Sep6BackEnd;
using Sep6BackEnd.DataAccess.DomainClasses.APIModels;
using Sep6BackEnd.DataAccess.TMDBAccess;

namespace UnitTestingSep6Backend;

public class UnitTest1
{
  
    [Fact]
    public void GetMovieByInvalidIdTest()
    {
        var keyProvider = Substitute.For<Keys>();

        

        //Arrange 
        var mocketHttpClient = Substitute.For<HttpClient>();
        var mocketTmdbAccess = new TmdbAccess(mocketHttpClient);
        
        //Act
        int invalidMovieID = 76600111;

        //Assert
        Assert.ThrowsAsync<TmdbException>(() => mocketTmdbAccess.GetMovie(invalidMovieID));
    }
    
    [Fact]
    public async Task GetMovieByValidIdTest()
    {

        var keyProvider = Substitute.For<Keys>();

        //Arrange 
        var mocketHttpClient = Substitute.For<HttpClient>();
        
        //Mocking the API response
        Movie avatar = new Movie
        {
            title = "Avatar: The Way of Water"
        };

        var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(avatar), Encoding.UTF8, "application/json")
        };

        mocketHttpClient.BaseAddress = new Uri($"https://api.themoviedb.org");

        var test = mocketHttpClient.GetAsync(Arg.Any<string>()).Returns(httpResponse);
        int id = 76600;
        var mocketTmdbAccess = Substitute.For<TmdbAccess>(mocketHttpClient);
        //Act
        var result = await mocketTmdbAccess.GetMovie(id);

        //Assert
        Assert.Equal("Avatar: The Way of Water", result.title);
    }
}