using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sep6BackEnd.Controllers;

namespace Sep6BackEnd.DataAccess.IMDBAccess
{
    public class TmdbAccess : ITmdbAccess
    {
        private HttpClient client;
        public TmdbAccess()
        { 
            client = new HttpClient();
        }
        
        public async Task<List<Movie>> getByTitle(string name)
        {
            string url = "https://api.themoviedb.org/3/search/movie?api_key="+ Keys._APIKEY + $"&language=en-US&query={name}&page=1&include_adult=false";

            string response = await client.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<Movie.Root>(response);

            return data.results;        
        }
        
        public async Task<List<Actor>> getByActorByName(string name)
        {
            string url = "https://api.themoviedb.org/3/search/person?api_key="+ Keys._APIKEY + $"&language=en-US&query={name}&page=1&include_adult=false";

            string response = await client.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<Actor.Root>(response);

            return data.results;
        }

        public async Task<List<Cast>> getMoviesByActor(string name)
        {
            //Getting Actor ID by String Name
            string Actorurl = "https://api.themoviedb.org/3/search/person?api_key="+ Keys._APIKEY + $"&language=en-US&query={name}&page=1&include_adult=false";
            string Actorresponse = await client.GetStringAsync(Actorurl);
            var ActorData = JsonConvert.DeserializeObject<Actor.Root>(Actorresponse);

            var ActorID = ActorData.results[0].id;

            //Getting list of Movies
            string url = $"https://api.themoviedb.org/3/person/{ActorID}/movie_credits?api_key="+ Keys._APIKEY + $"&language=en-US";
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Cast.Root>(response);
            return data.cast;
        }

        public async Task<List<Series>> getMostPopularSeries()
        {
            string url = $"https://api.themoviedb.org/3/trending/tv/week?api_key="+ Keys._APIKEY;
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Series.Root>(response);
            return data.results;
        }

        public async Task<List<Movie>> getMostPopularMovies()
        {
            string url = $"https://api.themoviedb.org/3/trending/movie/week?api_key="+ Keys._APIKEY;
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Movie.Root>(response);
            return data.results;
        }

        public async Task<List<Actor>> getMostPopularActors()
        {
            string url = $"https://api.themoviedb.org/3/trending/person/week?api_key="+ Keys._APIKEY;
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Actor.Root>(response);
            return data.results;
        }
    }
}