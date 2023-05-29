using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sep6BackEnd.DataAccess.DomainClasses.APIModels;

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

        public TmdbAccess(HttpClient client, Keys keys)
        {
            this.keys = keys;
            this.client = client;
        }

        public async Task<List<Movie>> GetMovieByTitle(string name)
        {
            try
            {
                string url = "https://api.themoviedb.org/3/search/movie?api_key="+ keys.APIKEY 
                    + $"&language=en-US&query={name}&page=1&include_adult=false";

                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }

                var response = await httpResponse.Content.ReadAsStringAsync();
                
                var data = JsonConvert.DeserializeObject<Movie.Root>(response);

                return data.results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<List<Actor>> GetByActorByName(string name)
        {
            try
            {
                string url = "https://api.themoviedb.org/3/search/person?api_key="+ keys.APIKEY 
                    + $"&language=en-US&query={name}&page=1&include_adult=false";
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }
                string response = await httpResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Actor.Root>(response);

                return data.results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<PersonDetails> GetActorById(int id)
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/person/{id}?api_key="+ keys.APIKEY + $"&language=en-US";
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }
                string response = await httpResponse.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<PersonDetails>(response);

                return data;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<MoviesByActor>> GetMoviesByActor(string name)
        {
            try
            {
                //Getting Actor ID by String Name
                string Actorurl = "https://api.themoviedb.org/3/search/person?api_key="+ keys.APIKEY 
                    + $"&language=en-US&query={name}&page=1&include_adult=false";
                HttpResponseMessage httpResponse = await client.GetAsync(Actorurl);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }
                
                string Actorresponse = await httpResponse.Content.ReadAsStringAsync();
                
                var ActorData = JsonConvert.DeserializeObject<Actor.Root>(Actorresponse);

                if (ActorData.results.Count == 0)
                {
                    return new List<MoviesByActor>();
                }
                var ActorID = ActorData.results[0].id;

                //Getting list of Movies
                string url = $"https://api.themoviedb.org/3/person/{ActorID}/movie_credits?api_key="+ keys.APIKEY 
                    + $"&language=en-US";
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }

                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                
                var data = JsonConvert.DeserializeObject<MoviesByActor.Root>(response);
                return data.cast;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<List<MoviesByActor>> GetMoviesByActorId(int id)
        {
            //Getting list of Movies
            try
            {
                string url = $"https://api.themoviedb.org/3/person/{id}/movie_credits?api_key="+ keys.APIKEY 
                    + $"&language=en-US";
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }
                string response = await httpResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MoviesByActor.Root>(response);
                return data.cast;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Series>> GetWeeklyTrendingSeries()
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/trending/tv/week?api_key="+ keys.APIKEY + $"&language=en-US";
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }
                
                var response = await httpResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Series.Root>(response);
                return data.results;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Movie>> GetMostPopularMovies()
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/movie/popular?api_key="+ keys.APIKEY + $"&language=en-US&page=1";
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponseMessage.StatusCode.ToString());
                }

                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Movie.Root>(response);
                return data.results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Movie>> GetUpcomingMovies()
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/movie/upcoming?api_key="+ keys.APIKEY + $"&language=en-US&page=1";
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponseMessage.StatusCode.ToString());
                }

                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Movie.Root>(response);
                return data.results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<List<Movie>> GetTopRatedMovies()
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/movie/top_rated?api_key="+ keys.APIKEY + $"&language=en-US&page=1";
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponseMessage.StatusCode.ToString());
                }

                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Movie.Root>(response);
                return data.results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<List<Movie>> GetWeeklyTrendingMovies()
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/trending/movie/week?api_key="+ keys.APIKEY + $"&language=en-US";
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponseMessage.StatusCode.ToString());
                }

                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Movie.Root>(response);
                return data.results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Actor>> GetMostPopularActors()
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/person/popular?api_key="+ keys.APIKEY +$"&language=en-US&page=1";
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }
                
                string response = await httpResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Actor.Root>(response);
                return data.results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<List<Actor>> GetWeeklyTrendingActors()
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/trending/person/week?api_key="+ keys.APIKEY 
                    + $"&language=en-US&page=1";
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }
                
                string response = await httpResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Actor.Root>(response);
                return data.results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Movie> GetMovie(int id)
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/movie/{id}?api_key="+keys.APIKEY+"&language=en-US";
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }
                string response = await httpResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Movie>(response);
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Cast>> GetActorByMovieId(int MovieID)
        {
            try
            {
                string url = $"https://api.themoviedb.org/3/movie/{MovieID}/credits?api_key="+keys.APIKEY+"&language=en-US";
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new TmdbException(httpResponse.StatusCode.ToString());
                }
                string response = await httpResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Cast.Root>(response);
                return data.cast;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}