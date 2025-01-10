using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Dto.SkillDtos
{
    public class UpdateSkillDto
    {
        public int SkillId { get; set; }
        public int AboutId { get; set; }
        public string? SkillName { get; set; }
    }
}
