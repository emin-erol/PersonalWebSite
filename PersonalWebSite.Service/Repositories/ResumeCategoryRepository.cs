using Microsoft.EntityFrameworkCore;
using PersonalWebSite.DAL.Core;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ItemTechViewModels;
using PersonalWebSite.Model.ViewModels.ResumeCategoryItemViewModels;
using PersonalWebSite.Model.ViewModels.ResumeCategoryViewModels;
using PersonalWebSite.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Repositories
{
    public class ResumeCategoryRepository : GenericRepository<ResumeCategory>, IResumeCategoryDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public ResumeCategoryRepository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(ResumeCategory entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<ResumeCategory>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<ResumeCategory> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(ResumeCategory entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(ResumeCategory entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<ResumeCategoryViewModel>> GetAllResumeContent()
        {
            var result = await _context.ResumeCategories
                .Include(rc => rc.ResumeCategoryItems)
                .ThenInclude(rci => rci.ItemTeches)
                .Select(rc => new ResumeCategoryViewModel
                {
                    ResumeCategoryId = rc.ResumeCategoryId,
                    CategoryName = rc.CategoryName,
                    ResumeCategoryItems = rc.ResumeCategoryItems.Select(rci => new ResumeCategoryItemViewModel
                    {
                        ResumeCategoryItemId = rci.ResumeCategoryItemId,
                        Title = rci.Title,
                        StartDate = rci.StartDate,
                        EndDate = rci.EndDate,
                        IconUrl = rci.IconUrl,
                        Header = rci.Header,
                        Description = rci.Description,
                        ResumeCategoryId = rc.ResumeCategoryId,
                        TechNames = rci.ItemTeches.Select(it =>
                            _context.TechsIUsed
                                .Where(t => t.TechIUsedId == it.TechIUsedId)
                                .Select(t => t.Name)
                                .FirstOrDefault()!
                        ).ToList()
                    }).ToList()
                }).ToListAsync();

            return result;
        }

    }
}
