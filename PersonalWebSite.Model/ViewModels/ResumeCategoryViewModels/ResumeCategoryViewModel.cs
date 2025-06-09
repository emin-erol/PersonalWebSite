using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ResumeCategoryItemViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.ViewModels.ResumeCategoryViewModels
{
    public class ResumeCategoryViewModel
    {
        public int ResumeCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UserId { get; set; }
        public List<ResumeCategoryItemViewModel> ResumeCategoryItems { get; set; }
    }
}
