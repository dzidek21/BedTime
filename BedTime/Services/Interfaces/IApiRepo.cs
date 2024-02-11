using BedTime.Models;

namespace BedTime.Services.Interfaces
{
    public interface IApiRepo
    {
        Task AddLike(int id);
        Task RemoveLike(int id);
        Task<List<TailModel>> GetAllTails();
        Task<int>GetLikesCount(int id);
        
        
    }
}
