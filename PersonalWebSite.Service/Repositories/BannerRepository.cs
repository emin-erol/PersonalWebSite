using Microsoft.EntityFrameworkCore;
using PersonalWebSite.DAL.Core;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.BannerViewModels;
using PersonalWebSite.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Repositories
{
    public class BannerRepository : GenericRepository<Banner>, IBannerDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public BannerRepository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(Banner entity)
        {
            await base.CreateAsync(entity);
        }

        public async Task<List<Banner>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Banner> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Banner entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Banner entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<GetBannerWithSocialMediaViewModel>> GetBannerWithSocialMedia()
        {
            var banners = await _context.Banners.ToListAsync();
            var socialMedias = await _context.SocialMedias.ToListAsync();

            var result = banners.Select(banner => new GetBannerWithSocialMediaViewModel
            {
                BannerId = banner.BannerId,
                MeetMessage = banner.MeetMessage,
                StartMessage = banner.StartMessage,
                Title = banner.Title,
                SocialMedias = socialMedias
            }).ToList();

            return result;
        }

        public async Task<List<GetBannerWithSocialMediaViewModel>> GetBannerWithSocialMediaByUserId(string userId)
        {
            var banners = await _context.Banners.Where(x => x.UserId == userId).ToListAsync();
            var socialMedias = await _context.SocialMedias.Where(x => x.UserId == userId).ToListAsync();

            var result = banners.Select(banner => new GetBannerWithSocialMediaViewModel
            {
                BannerId = banner.BannerId,
                MeetMessage = banner.MeetMessage,
                StartMessage = banner.StartMessage,
                Title = banner.Title,
                UserId = userId,
                SocialMedias = socialMedias
            }).ToList();

            return result;
        }
    }
}
