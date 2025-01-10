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
    public class TechIUsedRepository : GenericRepository<TechIUsed>, ITechIUsedDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public TechIUsedRepository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(TechIUsed entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<TechIUsed>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<TechIUsed> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(TechIUsed entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(TechIUsed entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<int> GetTechIUsedIdByTechName(string name)
        {
            var result = await _context.TechsIUsed
                .Where(t => t.Name == name)
                .Select(t => t.TechIUsedId)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
