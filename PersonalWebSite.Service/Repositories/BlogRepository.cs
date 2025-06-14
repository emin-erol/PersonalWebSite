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
    public class BlogRepository : GenericRepository<Blog>, IBlogDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public BlogRepository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(Blog entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Blog entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Blog entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<Blog>> GetBlogsByUserId(string userId)
        {
            var blogs = await _context.Blogs.Where(x => x.UserId == userId).ToListAsync();

            return blogs;
        }
    }
}
