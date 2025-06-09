using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.BannerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Interfaces
{
    public interface IBannerDal : IGenericDal<Banner>
    {
        Task<List<GetBannerWithSocialMediaViewModel>> GetBannerWithSocialMedia();
        Task<List<GetBannerWithSocialMediaViewModel>> GetBannerWithSocialMediaByUserId(string userId);
    }
}
