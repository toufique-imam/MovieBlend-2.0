using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieBlend.Models;
using Newtonsoft.Json;
using RestSharp;
namespace MovieBlend.Controllers
{
    public class HomeController : Controller
    {
        public static TMDB tMDB = new TMDB();
        public static AllData allData = new AllData();
        public IActionResult Index()
        {
            var client = new RestClient(tMDB.on_air_tv());
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<TVArray>(response.Content);
                
                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                    temp.results[i].media_type = "tv";
                }
                allData.tVArray = temp;
            }
            client = new RestClient(tMDB.on_theater());
            request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<MovieArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].media_type = "movie";
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                }
                allData.movieArray = temp;
            }
            return View(allData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Search(AllData allData)
        {
            Console.WriteLine(allData.search_data);
            var client = new RestClient(tMDB.makequery_movie(allData.search_data));
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<MovieArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].media_type = "movie";
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                }
                allData.movieArray = temp;
            }

            client = new RestClient(tMDB.makequery_tv(allData.search_data));
            request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<TVArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                    temp.results[i].media_type = "tv";
                }
                allData.tVArray = temp;
            }
            return View(allData);
        }

        public IActionResult DetailsMovie(string data)
        {
            int id = to_int(data);
            var client = new RestClient(tMDB.movie_details(id));
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            MovieData temp=new MovieData();
            if (response.IsSuccessful)
            {
                temp = JsonConvert.DeserializeObject<MovieData>(response.Content);
                temp.poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.poster_path;
                temp.backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.backdrop_path;
            }
            return View(temp);
            
        }
        public int to_int(string s)
        {
            int ans = 0;
            for(int i = 0; i < s.Length; i++)
            {
                ans = ans * 10 + s[i] - '0';
            }
            return ans;
        }
        public IActionResult DetailsTV(string data)
        {
            int id = to_int(data);
            var client = new RestClient(tMDB.TV_details(id));
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            TVData temp = new TVData();
            if (response.IsSuccessful)
            {
                temp = JsonConvert.DeserializeObject<TVData>(response.Content);
                temp.poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.poster_path;
                temp.backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.backdrop_path;
            }
            return View(temp);
            
        }
        public IActionResult PopularMovie()
        {
            var client = new RestClient(tMDB.get_popular_movies());
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<MovieArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].media_type = "movie";
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                }
                allData.movieArray = temp;
                return View(allData);

            }
            else return null;

        }
        public IActionResult Top_Movie()
        {
            var client = new RestClient(tMDB.get_top_movie());
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<MovieArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].media_type = "movie";
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                }
                allData.movieArray = temp;
                return View("PopularMovie",allData);

            }
            else return null;
        }
        public IActionResult Upcoming()
        {
            var client = new RestClient(tMDB.upcomingMovie());
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<MovieArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].media_type = "movie";
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                }
                allData.movieArray = temp;
                return View("PopularMovie", allData);

            }
            else return null;
        }
        public IActionResult Now_Playing()
        {
            var client = new RestClient(tMDB.now_playing_movie());
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<MovieArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].media_type = "movie";
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                }
                allData.movieArray = temp;
                return View("PopularMovie", allData);

            }
            else return null;
        }
        public IActionResult PopularTV()
        {
            var client = new RestClient(tMDB.get_popular_tv());
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<TVArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                    temp.results[i].media_type = "tv";
                }
                allData.tVArray = temp;
            }
            return View(allData);
        }
        public IActionResult Top_TV()
        {
            var client = new RestClient(tMDB.get_top_tv());
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<TVArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                    temp.results[i].media_type = "tv";
                }
                allData.tVArray = temp;
            }
            return View("PopularTV",allData);
        }
        public IActionResult ON_TV()
        {
            var client = new RestClient(tMDB.get_on_tv());
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<TVArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                    temp.results[i].media_type = "tv";
                }
                allData.tVArray = temp;
            }
            return View("PopularTV", allData);
        }
        public IActionResult Airing_today()
        {
            var client = new RestClient(tMDB.get_air_tv());
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var temp = JsonConvert.DeserializeObject<TVArray>(response.Content);

                for (int i = 0; i < temp.results.Length; i++)
                {
                    temp.results[i].poster_path = "https://image.tmdb.org/t/p/w300_and_h450_bestv2" + temp.results[i].poster_path;
                    temp.results[i].backdrop_path = "https://image.tmdb.org/t/p/w1400_and_h450_face" + temp.results[i].backdrop_path;
                    temp.results[i].media_type = "tv";
                }
                allData.tVArray = temp;
            }
            return View("PopularTV", allData);
        }
    }
}
