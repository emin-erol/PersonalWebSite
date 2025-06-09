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
    public class ContactInfoRepository : GenericRepository<ContactInfo>, IContactInfoDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public ContactInfoRepository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(ContactInfo entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<ContactInfo>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<ContactInfo> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(ContactInfo entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(ContactInfo entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<ContactInfo>> GetContactInfosByUserId(string userId)
        {
            var contactInfos = await _context.ContactInfos.Where(x => x.UserId == userId).ToListAsync();

            return contactInfos;
        }
    }
}
