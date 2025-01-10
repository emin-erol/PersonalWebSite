using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.ViewModels.SkillViewModels
{
    public class UpdateSkillViewModel
    {
        public int SkillId { get; set; }
        public int AboutId { get; set; }
        public string? SkillName { get; set; }
    }
}
