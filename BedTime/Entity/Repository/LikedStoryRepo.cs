using BedTime.Entity.Interfaces;
using BedTime.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedTime.Entity.Repository
{
    public class LikedStoryRepo : ILikedStoryRepo
    {
        protected readonly DatabaseContext _context;

        public LikedStoryRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> AddLikedStory(LikedStory likedStory)
        {
            var add = await _context.LikedStories.AddAsync(likedStory);
            await _context.SaveChangesAsync();
            return add.Entity.Id;
        }

        public async Task<int> DeleteLikedStory(int id)
        {
            var model = await _context.LikedStories.FindAsync(id);
            if (model != null)
            {
                _context.LikedStories.Remove(model);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }


        public async Task<List<LikedStory>> GetLikedStories()
        {
            return await _context.LikedStories.ToListAsync();
        }

        public async Task<LikedStory> GetLikedStory(int id)
        {
            var model= await _context.LikedStories.FirstOrDefaultAsync(x=>x.StoryId==id);
            if (model == null)
            {
                return new LikedStory();
            }
            return model;
        }


        public async Task<int> UpdateLikedStory(LikedStory likedStory)
        {
            _context.LikedStories.Update(likedStory);
            return await _context.SaveChangesAsync();
        }
    }
}
