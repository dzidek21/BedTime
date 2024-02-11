using BedTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedTime.Entity.Interfaces
{
    public interface IFairTalesRepository
    {
        Task<IEnumerable<TailModel>> GetAllTails();
        Task <TailModel> GetTailById(int id);
        Task<bool> AddToFavorites(TailModel model);
        Task RemoveFromFavorites(int id);
        Task<IEnumerable<TailModel>>GetAllFavorites();
        Task AddNewTail(TailModel tail);
        Task UpdateTail(TailModel tail);
        
    }
}
