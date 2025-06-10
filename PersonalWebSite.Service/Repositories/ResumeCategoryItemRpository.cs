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
                                where rci.ResumeCategoryItemId == id
                                select new
                                {
                                    rci.ResumeCategoryItemId,
                                    rci.Title,
                                    rci.StartDate,
                                    rci.EndDate,
                                    rci.IconUrl,
                                    rci.Header,
                                    rci.Description,
                                    rc.ResumeCategoryId,
                                    rc.CategoryName,
                                    Techs = (from it in _context.ItemTeches
                                             join ti in _context.TechsIUsed on it.TechIUsedId equals ti.TechIUsedId
                                             where it.ResumeCategoryItemId == rci.ResumeCategoryItemId
                                             select ti.Name).ToList()
                                }).FirstOrDefaultAsync();

            if (result == null)
                return null;

            return new ResumeCategoryItemsWithTechIUsedViewModel
            {
                ResumeCategoryItemId = result.ResumeCategoryItemId,
                Title = result.Title,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                IconUrl = result.IconUrl,
                Header = result.Header,
                Description = result.Description,
                ResumeCategoryId = result.ResumeCategoryId,
                ResumeCategoryName = result.CategoryName,
                TechNames = result.Techs ?? new List<string>()
            };
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

        public async Task<List<ResumeCategoryItemsWithTechIUsedViewModel>> GetResumeCategoryItemsWithTechIUsedByUserId(string userId)
        {
            var result = await (from rci in _context.ResumeCategoryItems
                                join rc in _context.ResumeCategories on rci.ResumeCategoryId equals rc.ResumeCategoryId
                                where rc.UserId == userId
                                join it in _context.ItemTeches on rci.ResumeCategoryItemId equals it.ResumeCategoryItemId into itemTechGroup
                                from it in itemTechGroup.DefaultIfEmpty()
                                join ti in _context.TechsIUsed on it.TechIUsedId equals ti.TechIUsedId into techGroup
                                from ti in techGroup.DefaultIfEmpty()
                                select new
                                {
                                    rci.ResumeCategoryItemId,
                                    rci.Title,
                                    rci.StartDate,
                                    rci.EndDate,
                                    rci.IconUrl,
                                    rci.Header,
                                    rci.Description,
                                    rc.ResumeCategoryId,
                                    rc.CategoryName,
                                    TechName = ti != null ? ti.Name : null
                                })
                                .GroupBy(x => new
                                {
                                    x.ResumeCategoryItemId,
                                    x.Title,
                                    x.StartDate,
                                    x.EndDate,
                                    x.IconUrl,
                                    x.Header,
                                    x.Description,
                                    x.ResumeCategoryId,
                                    x.CategoryName
                                })
                                .Select(g => new ResumeCategoryItemsWithTechIUsedViewModel
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
                                    TechNames = g.Where(x => x.TechName != null).Select(x => x.TechName).Distinct().ToList()
                                })
                                .ToListAsync();

            return result;
        }

    }
}
