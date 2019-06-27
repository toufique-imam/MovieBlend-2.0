using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieBlend.Models;
using MovieBlend.Services;
namespace MovieBlend.Controllers
{
    public class PostController : Controller
    {
        private readonly IDiscussionDataService discussionDataService;
        private readonly UserManager<IdentityUser> _usermanger;
        public static string moviename;
        public static DIscussionModelArray DMA = new DIscussionModelArray();
        public static int movie_id;
        public PostController(IDiscussionDataService discussionData,UserManager<IdentityUser>userManager)
        {
            _usermanger = userManager;
            discussionDataService = discussionData;
        }
        public async Task<IActionResult> Index()
        {
            DMA.Data=await discussionDataService.GetDataAsync();
            DMA.MovieName = "Welcome";
            return View("Index",DMA);
        }
        public async Task<IActionResult> MediaDisCussion(string data)
        {
            int idm = to_int(data);
            movie_id = idm;
            Console.WriteLine(idm);
            DMA.Data =await discussionDataService.GetDataForMovie(idm);
            DMA.MovieName = moviename;
            return View("Index", DMA);
            DMA.MovieId = movie_id;
        }
        public IActionResult fill_form(string data)
        {
            int idm = to_int(data);
            movie_id = idm;
            return View("AddDiscussion");
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddDiscussion(DiscussionModel dummyData)
        {
            dummyData.Id = Guid.NewGuid();
            dummyData.Movie_ID = movie_id;
            dummyData.Movie_name = moviename;
            var currentUser = await _usermanger.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            dummyData.Poster_ID = currentUser.Id;
            dummyData.poster_name = currentUser.UserName;
            var res=await discussionDataService.AddDiscussion(dummyData);
            return await DetailDiscussion(dummyData.Id.ToString());
         
        }
        public async Task<IActionResult> DeleteDiscussion(string data)
        {
            var xx = await discussionDataService.GetDataByidAsync(data);
            var res = await discussionDataService.DeleteData(xx);
            return await Index();
            
        }
        public async Task<IActionResult> DetailDiscussion(string idd)
        {
            DiscussionModel discussionModel = await discussionDataService.GetDataByidAsync(idd);
            return View("DetailDiscussion", discussionModel);
        }
        [Authorize]
        public async Task<IActionResult> get_my_data()
        {
            var currentUser = await _usermanger.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            DMA.Data = await discussionDataService.MyDiscussion(currentUser.Id);
            DMA.MovieName = "Welcome";
            return View("Index", DMA);
            
        }
        public int to_int(string s)
        {
            int ans = 0;
            int j = 0;
            for (int i = 0; i < s.Length; i++)
            {
                j = i;
                if (Char.IsDigit(s[i]))
                    ans = ans * 10 + s[i] - '0';
                else break;
            }
            moviename = "";
            while (j < s.Length)
            {
                moviename += s[j];
                j++;
            }
            return ans;
        }

    }
}