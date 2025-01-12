using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ResumeCategoryItemViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Interfaces
{
    public interface IResumeCategoryItemDal : IGenericDal<ResumeCategoryItem>
    {
        Task<List<ResumeCategoryItemsWithTechIUsedViewModel>> GetResumeCategoryItemsWithTechIUsed();
        Task<ResumeCategoryItemsWithTechIUsedViewModel> GetResumeCategoryItemWithTechIUsedByResumeCategoryItemId(int id);
        Task<List<ResumeCategoryItemViewModel>> GetResumeCategoryItemsByResumeCategoryId(int resumeCategoryId);
    }
}
