using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.SkillViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.ViewModels.AboutViewModels
{
    public class CreateAboutViewModel
    {
        public string? StartMessage { get; set; }
        public string? ProfileMessage { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? CvLink { get; set; }
        public List<CreateSkillViewModel> Skills { get; set; }
    }
}
