using System.Net;
using Newtonsoft.Json;
using NSubstitute;
using Sep6BackEnd;
using Sep6BackEnd.DataAccess.DomainClasses.APIModels;
using Sep6BackEnd.DataAccess.TMDBAccess;

namespace UnitTestingSep6Backend;

public class TmdbAccesUnitTest
{
    [Fact]
    public void TmdbAcces_WhenRequstingInvalidMovie_ShouldThrow()
    {
        var keyProvider = Substitute.For<Keys>();
        
        //Arrange 
        var messageHandler = new MockHttpMessageHandler("", HttpStatusCode.NotFound);
        var mocketHttpClient = new HttpClient(messageHandler);
        var tmdbAccess = new TmdbAccess(mocketHttpClient, keyProvider);
        
        //Act
        int invalidMovieID = 76600111;

        //Assert
        Assert.ThrowsAsync<TmdbException>(() => tmdbAccess.GetMovie(invalidMovieID));
    }
    
    [Fact]
    public async Task GetMovieByValidIdTest()
    {
        var keyProvider = Substitute.For<Keys>();
        
        //Arrange
        Movie avatar = new Movie
        {
            title = "Avatar: The Way of Water"
        };

        var messageHandler = new MockHttpMessageHandler(JsonConvert.SerializeObject(avatar), HttpStatusCode.OK);
        var mocketHttpClient = new HttpClient(messageHandler);

        int id = 76600;
        var tmdbAccess = new TmdbAccess(mocketHttpClient, keyProvider);
        
        //Act
        var result = await tmdbAccess.GetMovie(id);

        //Assert
        Assert.Equal("Avatar: The Way of Water", result.title);
    }
}