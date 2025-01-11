using PersonalWebSite.Model.ViewModels.SkillViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.ViewModels.AboutViewModels
{
    public class UpdateAboutViewModel
    {
        public int AboutId { get; set; }
        public string? StartMessage { get; set; }
        public string? ProfileMessage { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? CvLink { get; set; }
        public string? ProfileImageLink { get; set; }
        public List<UpdateSkillViewModel> Skills { get; set; }
    }
}
