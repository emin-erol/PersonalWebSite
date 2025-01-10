using Microsoft.EntityFrameworkCore;
using PersonalWebSite.DAL.Core;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Repositories
{
    public class ItemTechRepository : GenericRepository<ItemTech>, IItemTechDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public ItemTechRepository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(ItemTech entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<ItemTech>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<ItemTech> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(ItemTech entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(ItemTech entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<int> GetItemTechIdByResumeCategoryItemIdAndTechIUsedName(int resumeCategoryItemId, string name)
        {
            var techIUsedId = await _context.TechsIUsed
                .Where(t => t.Name == name)
                .Select(t => t.TechIUsedId)
                .FirstOrDefaultAsync();

            var itemTechId = await _context.ItemTeches
                .Where(it => it.ResumeCategoryItemId == resumeCategoryItemId && it.TechIUsedId == techIUsedId)
                .Select(it => it.ItemTechId)
                .FirstOrDefaultAsync();

            return itemTechId;
        }
    }
}
