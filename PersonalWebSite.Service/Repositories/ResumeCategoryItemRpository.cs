using Microsoft.EntityFrameworkCore;
using PersonalWebSite.DAL.Core;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ResumeCategoryItemViewModels;
using PersonalWebSite.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Repositories
{
    public class ResumeCategoryItemRpository : GenericRepository<ResumeCategoryItem>, IResumeCategoryItemDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public ResumeCategoryItemRpository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(ResumeCategoryItem entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<ResumeCategoryItem>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<ResumeCategoryItem> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

		public async Task RemoveAsync(ResumeCategoryItem entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(ResumeCategoryItem entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<ResumeCategoryItemsWithTechIUsedViewModel>> GetResumeCategoryItemsWithTechIUsed()
        {
            var result = await (from rci in _context.ResumeCategoryItems
                                join rc in _context.ResumeCategories on rci.ResumeCategoryId equals rc.ResumeCategoryId
                                join it in _context.ItemTeches on rci.ResumeCategoryItemId equals it.ResumeCategoryItemId
                                join ti in _context.TechsIUsed on it.TechIUsedId equals ti.TechIUsedId
                                group ti by new
                                {
                                    rci.ResumeCategoryItemId,
                                    rci.Title,
                                    rci.StartDate,
                                    rci.EndDate,
                                    rci.IconUrl,
                                    rci.Header,
                                    rci.Description,
                                    rc.ResumeCategoryId,
                                    rc.CategoryName
                                } into g
                                select new ResumeCategoryItemsWithTechIUsedViewModel
                                {
                                    ResumeCategoryItemId = g.Key.ResumeCategoryItemId,
                                    Title = g.Key.Title,
                                    StartDate = g.Key.StartDate,
                                    EndDate = g.Key.EndDate,
                                    IconUrl = g.Key.IconUrl,
                                    Header = g.Key.Header,
                                    Description = g.Key.Description,
                                    ResumeCategoryId = g.Key.ResumeCategoryId,
                                    ResumeCategoryName = g.Key.CategoryName,
                                    TechNames = g.Select(x => x.Name).ToList()
                                }).ToListAsync();

            return result;
        }

        public async Task<ResumeCategoryItemsWithTechIUsedViewModel> GetResumeCategoryItemWithTechIUsedByResumeCategoryItemId(int id)
        {
            var result = await (from rci in _context.ResumeCategoryItems
                                join rc in _context.ResumeCategories on rci.ResumeCategoryId equals rc.ResumeCategoryId
                                join it in _context.ItemTeches on rci.ResumeCategoryItemId equals it.ResumeCategoryItemId
                                join ti in _context.TechsIUsed on it.TechIUsedId equals ti.TechIUsedId
                                where rci.ResumeCategoryItemId == id
                                group ti by new
                                {
                                    rci.ResumeCategoryItemId,
                                    rci.Title,
                                    rci.StartDate,
                                    rci.EndDate,
                                    rci.IconUrl,
                                    rci.Header,
                                    rci.Description,
                                    rc.ResumeCategoryId,
                                    rc.CategoryName
                                } into g
                                select new ResumeCategoryItemsWithTechIUsedViewModel
                                {
                                    ResumeCategoryItemId = g.Key.ResumeCategoryItemId,
                                    Title = g.Key.Title,
                                    StartDate = g.Key.StartDate,
                                    EndDate = g.Key.EndDate,
                                    IconUrl = g.Key.IconUrl,
                                    Header = g.Key.Header,
                                    Description = g.Key.Description,
                                    ResumeCategoryId = g.Key.ResumeCategoryId,
                                    ResumeCategoryName = g.Key.CategoryName,
                                    TechNames = g.Select(x => x.Name).ToList()
                                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<ResumeCategoryItemViewModel>> GetResumeCategoryItemsByResumeCategoryId(int resumeCategoryId)
        {
            var values = await _context.ResumeCategoryItems
                .Where(rci => rci.ResumeCategoryId == resumeCategoryId)
                .Select(rci => new ResumeCategoryItemViewModel
                {
                    ResumeCategoryItemId = rci.ResumeCategoryItemId,
                    ResumeCategoryId = resumeCategoryId,
                    Description = rci.Description,
                    Header = rci.Header,
                    IconUrl = rci.IconUrl,
                    Title = rci.Title,
                    StartDate = rci.StartDate,
                    EndDate = rci.EndDate
                }).ToListAsync();

            return values;
        }
    }
}
