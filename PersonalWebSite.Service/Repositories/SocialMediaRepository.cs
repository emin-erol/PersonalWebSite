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
    public class SocialMediaRepository : GenericRepository<SocialMedia>, ISocialMediaDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public SocialMediaRepository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(SocialMedia entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<SocialMedia>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<SocialMedia> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(SocialMedia entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(SocialMedia entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<SocialMedia>> GetSocialMediasByUserId(string userId)
        {
            var socialMedias = _context.SocialMedias.Where(x => x.UserId == userId).ToList();

            return socialMedias;
        }
    }
}
