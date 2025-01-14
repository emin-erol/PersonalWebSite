﻿using Microsoft.EntityFrameworkCore;
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
    public class ContactMailRepository : GenericRepository<ContactMail>, IContactMailDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public ContactMailRepository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(ContactMail entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<ContactMail>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<ContactMail> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task MarkAsRead(int contactMailId)
        {
            var contactMail = await _context.ContactMails.FirstOrDefaultAsync(cm => cm.ContactMailId == contactMailId);

            if (contactMail != null)
            {
                contactMail.IsRead = true;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"ContactMail with ID {contactMailId} not found.");
            }
        }
    }
}
