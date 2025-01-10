using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string? SkillName { get; set; }
        public int AboutId { get; set; }
        public About About { get; set; }
    }
}
