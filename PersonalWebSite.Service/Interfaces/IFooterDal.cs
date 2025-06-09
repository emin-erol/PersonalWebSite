using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.FooterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Interfaces
{
    public interface IFooterDal : IGenericDal<Footer>
    {
        Task<List<GetFooterWithSocialMediaViewModel>> GetFooterWithSocialMedia();
        Task<List<GetFooterWithSocialMediaViewModel>> GetFooterWithSocialMediaByUserId(string userId);
    }
}
