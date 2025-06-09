using Microsoft.EntityFrameworkCore;
using PersonalWebSite.DAL.Core;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.BannerViewModels;
using PersonalWebSite.Model.ViewModels.FooterViewModels;
using PersonalWebSite.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Repositories
{
    public class FooterRepository : GenericRepository<Footer>, IFooterDal
    {
        private readonly PersonalWebSiteDbContext _context;
        public FooterRepository(PersonalWebSiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(Footer entity)
        {
            await base.CreateAsync(entity);
        }

		public async Task<List<Footer>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Footer> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task RemoveAsync(Footer entity)
        {
            await base.RemoveAsync(entity);
        }

        public async Task UpdateAsync(Footer entity)
        {
            await base.UpdateAsync(entity);
		}

		public async Task<List<GetFooterWithSocialMediaViewModel>> GetFooterWithSocialMedia()
		{
			var footers = await _context.Footers.ToListAsync();
			var socialMedias = await _context.SocialMedias.ToListAsync();

			var result = footers.Select(footer => new GetFooterWithSocialMediaViewModel
			{
                FooterId = footer.FooterId,
				Description = footer.Description,
				SocialMedias = socialMedias
			}).ToList();

			return result;
		}

        public async Task<List<GetFooterWithSocialMediaViewModel>> GetFooterWithSocialMediaByUserId(string userId)
        {
            var footers = await _context.Footers.Where(x => x.UserId == userId).ToListAsync();
            var socialMedias = await _context.SocialMedias.Where(x => x.UserId == userId).ToListAsync();

            var result = footers.Select(footer => new GetFooterWithSocialMediaViewModel
            {
                FooterId = footer.FooterId,
                Description = footer.Description,
                UserId = userId,
                SocialMedias = socialMedias
            }).ToList();

            return result;
        }
    }
}
