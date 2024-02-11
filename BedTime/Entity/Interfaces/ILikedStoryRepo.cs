using BedTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedTime.Entity.Interfaces
{
    public interface ILikedStoryRepo
    {
        Task<LikedStory> GetLikedStory(int id);
        Task<List<LikedStory>> GetLikedStories();
        Task<int> AddLikedStory(LikedStory likedStory);
        Task<int> DeleteLikedStory(int id);
        Task<int> UpdateLikedStory(LikedStory likedStory);
    }
}
