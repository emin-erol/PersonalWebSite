using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ResumeCategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Interfaces
{
    public interface IResumeCategoryDal : IGenericDal<ResumeCategory>
    {
        Task<List<ResumeCategoryViewModel>> GetAllResumeContent();
    }
}
