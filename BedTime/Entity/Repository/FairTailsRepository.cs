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
    public class FairTailsRepository : IFairTalesRepository
    {
        private readonly DatabaseContext _context;

        public FairTailsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddNewTail(TailModel tail)
        {
            await _context.Tails.AddAsync(tail);
            _context.SaveChanges();
        }

        public async Task<bool> AddToFavorites(TailModel model)
        {
            var findModel = await _context.Tails.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (findModel == null)
            {
                model.Favorites = true;
                await AddNewTail(model);
                return true;
            }
            else
            {
                model.Favorites = false;
                await RemoveFromFavorites(model.Id);
                return false;
            }
        }

        public async Task<IEnumerable<TailModel>> GetAllFavorites()
        {
            var modelList=await _context.Tails.Where(x=>x.Favorites==true).ToListAsync();
            return modelList;
        }

        public async Task<IEnumerable<TailModel>> GetAllTails()
        {
            return await _context.Tails.ToListAsync();
        }

        public async Task<TailModel> GetTailById(int id)
        {
            var model=await _context.Tails.FirstOrDefaultAsync(x=>x.Id == id);
            if (model == null)
            {
                return new TailModel();
            }
            return model;
        }

        public async Task RemoveFromFavorites(int id)
        {
            var model= await _context.Tails.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return;
            }
            model.Favorites= false;
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTail(TailModel tail)
        {
            var model = await _context.Tails.FirstOrDefaultAsync(t => t.Id == tail.Id);
            if (model == null)
            {
                return;
            }
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
