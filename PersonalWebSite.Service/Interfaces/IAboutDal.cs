using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.AboutViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Interfaces
{
    public interface IAboutDal : IGenericDal<About>
    {
        Task<List<GetAboutWithSkillViewModel>> GetAboutWithSkill(string userId);
        Task<GetAboutWithSkillViewModel> GetAboutWithSkillByAboutId(int id);
        Task<string> GetProfileImageLink(string userId);
        Task<List<GetAboutWithSkillViewModel>> GetAboutWithSkillByUserId(string userId);
    }
}
