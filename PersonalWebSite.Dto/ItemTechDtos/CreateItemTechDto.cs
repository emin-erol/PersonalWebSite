using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Dto.ItemTechDtos
{
    public class CreateItemTechDto
    {
        public string ItemTechName { get; set; }
        public int ResumeCategoryItemId { get; set; }
        public int TechIUsedId { get; set; }
    }
}
