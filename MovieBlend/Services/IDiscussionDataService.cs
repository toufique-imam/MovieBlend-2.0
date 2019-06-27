using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBlend.Models;
namespace MovieBlend.Services
{
    public interface IDiscussionDataService
    {
        Task<DiscussionModel> GetDataByidAsync(string id);
        Task<DiscussionModel[]> GetDataAsync();
        Task<DiscussionModel[]> GetDataForMovie(int mid);
        Task<bool> AddDiscussion(DiscussionModel data);
        Task<bool> UpdateData(DiscussionModel data);
        Task<bool> DeleteData(DiscussionModel data);
        Task<DiscussionModel[]> MyDiscussion(string id);
    }
}
