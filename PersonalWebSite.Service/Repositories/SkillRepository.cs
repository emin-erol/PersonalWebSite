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
    public class SkillRepository : GenericRepository<Skill>, ISkillDal
    {
        public SkillRepository(PersonalWebSiteDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Skill entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Skill>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Skill entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Skill entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
