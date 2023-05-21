using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sep6BackEnd.Controllers;
using Sep6BackEnd.DataAccess.IMDBAccess;

namespace Sep6BackEnd.DataAccess.TMDBAccess
{
    public class TmdbAccess : ITmdbAccess
    {
        private HttpClient client;
        private readonly Keys keys;
        public TmdbAccess(Keys keys)
        { 
            client = new HttpClient();
            this.keys = keys;
        }
        
        public async Task<List<Movie>> GetByTitle(string name)
        {
            string url = "https://api.themoviedb.org/3/search/movie?api_key="+ keys.APIKEY + $"&language=en-US&query={name}&page=1&include_adult=false";

            string response = await client.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<Movie.Root>(response);

            return data.results;        
        }
        
        public async Task<List<Actor>> GetByActorByName(string name)
        {
            string url = "https://api.themoviedb.org/3/search/person?api_key="+ keys.APIKEY + $"&language=en-US&query={name}&page=1&include_adult=false";

            string response = await client.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<Actor.Root>(response);

            return data.results;
        }

        public async Task<PersonDetails> GetActorById(int id)
        {
            string url = $"https://api.themoviedb.org/3/person/{id}?api_key="+ keys.APIKEY;

            string response = await client.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<PersonDetails>(response);

            return data;
        }

        public async Task<List<MoviesByActor>> GetMoviesByActor(string name)
        {
            //Getting Actor ID by String Name
            string Actorurl = "https://api.themoviedb.org/3/search/person?api_key="+ keys.APIKEY + $"&language=en-US&query={name}&page=1&include_adult=false";
            string Actorresponse = await client.GetStringAsync(Actorurl);
            var ActorData = JsonConvert.DeserializeObject<Actor.Root>(Actorresponse);

            if (ActorData.results.Count == 0)
            {
                return new List<MoviesByActor>();
            }
            var ActorID = ActorData.results[0].id;
            

            //Getting list of Movies
            string url = $"https://api.themoviedb.org/3/person/{ActorID}/movie_credits?api_key="+ keys.APIKEY + $"&language=en-US";
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<MoviesByActor.Root>(response);
            return data.cast;
            
        }

        public async Task<List<MoviesByActor>> GetMoviesByActorId(int id)
        {
            //Getting list of Movies
            string url = $"https://api.themoviedb.org/3/person/{id}/movie_credits?api_key="+ keys.APIKEY + $"&language=en-US";
            try
            {
                string response = await client.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<MoviesByActor.Root>(response);
                return data.cast;
            }
            catch (Exception e)
            {
                return new List<MoviesByActor>();
            }
            
        }

        public async Task<List<Series>> GetMostPopularSeries()
        {
            string url = $"https://api.themoviedb.org/3/trending/tv/week?api_key="+ keys.APIKEY;
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Series.Root>(response);
            return data.results;
        }

        public async Task<List<Movie>> GetMostPopularMovies()
        {
            string url = $"https://api.themoviedb.org/3/trending/movie/week?api_key="+ keys.APIKEY;
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Movie.Root>(response);
            return data.results;
        }

        public async Task<List<Actor>> GetMostPopularActors()
        {
            string url = $"https://api.themoviedb.org/3/trending/person/week?api_key="+ keys.APIKEY;
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Actor.Root>(response);
            return data.results;
        }

        public async Task<Movie> GetMovie(int id)
        {
            string url = $"https://api.themoviedb.org/3/movie/{id}?api_key="+keys.APIKEY+"&language=en-US";
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Movie>(response);
            return data;
        }

        public async Task<List<Cast>> GetActorByMovie(int MovieID)
        {
            string url = $"https://api.themoviedb.org/3/movie/{MovieID}/credits?api_key="+keys.APIKEY+"&language=en-US";
            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<Cast.Root>(response);
            return data.cast;
        }
    }
}