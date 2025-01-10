using Microsoft.EntityFrameworkCore;
using PersonalWebSite.DAL.Core;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.AboutViewModels;
using PersonalWebSite.Model.ViewModels.SkillViewModels;
using PersonalWebSite.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Repositories
{
    public class AboutRepository : GenericRepository<About>, IAboutDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public AboutRepository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(About entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<About>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(About entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(About entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<GetAboutWithSkillViewModel>> GetAboutWithSkill()
        {
            var result = await _context.Abouts
                .Include(x => x.Skills)
                .Select(x => new GetAboutWithSkillViewModel
                {
                    AboutId = x.AboutId,
                    StartMessage = x.StartMessage,
                    ProfileMessage = x.ProfileMessage,
                    BirthDate = x.BirthDate,
                    Title = x.Title,
                    Email = x.Email,
                    CvLink = x.CvLink,
                    Skills = x.Skills.Select(s => new SkillViewModel
                    {
                        SkillId = s.SkillId,
                        SkillName = s.SkillName
                    }).ToList()
                })
                .ToListAsync();

            return result;
        }

        public async Task<GetAboutWithSkillViewModel> GetAboutWithSkillByAboutId(int id)
        {
            var result = await _context.Abouts
                .Where(a => a.AboutId == id)
                .Include(x => x.Skills)
                .Select(x => new GetAboutWithSkillViewModel
                {
                    AboutId = x.AboutId,
                    StartMessage = x.StartMessage,
                    ProfileMessage = x.ProfileMessage,
                    BirthDate = x.BirthDate,
                    Title = x.Title,
                    Email = x.Email,
                    CvLink = x.CvLink,
                    Skills = x.Skills.Select(s => new SkillViewModel
                    {
                        SkillId = s.SkillId,
                        AboutId= s.AboutId,
                        SkillName = s.SkillName
                    }).ToList()
                }).FirstOrDefaultAsync();

            return result;
        }
    }
}
