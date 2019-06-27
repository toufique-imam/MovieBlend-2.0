using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieBlend.Data;
using MovieBlend.Models;

namespace MovieBlend.Services
{
    public class DiscussionDataService : IDiscussionDataService
    {
        private readonly ApplicationDbContext _context;
        public DiscussionDataService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddDiscussion(DiscussionModel data)
        {
            _context.DiscussionData.Add(data);
            var res = await _context.SaveChangesAsync();
            return res == 1;
        }

        public async Task<bool> DeleteData(DiscussionModel data)
        {
            _context.DiscussionData.Remove(data);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<DiscussionModel[]> GetDataAsync()
        {
            return await _context.DiscussionData.ToArrayAsync();
            //throw new NotImplementedException();
        }

        public async Task<DiscussionModel> GetDataByidAsync(string id)
        {
            return await _context.DiscussionData.FirstAsync(x => x.Id.ToString() == id);

        }

        public async Task<DiscussionModel[]> GetDataForMovie(int mid)
        {
            return await _context.DiscussionData
                .Where(x => x.Movie_ID == mid).ToArrayAsync();
  
        }

        public async Task<DiscussionModel[]> MyDiscussion(string id)
        {
            return await _context.DiscussionData
                .Where(x => x.Poster_ID == id)
                .ToArrayAsync();
        }

        public async Task<bool> UpdateData(DiscussionModel data)
        {
            _context.DiscussionData.Update(data);
            return await _context.SaveChangesAsync() == 1;
            //throw new NotImplementedException();
        }
    }
}
