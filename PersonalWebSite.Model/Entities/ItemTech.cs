using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.Entities
{
    public class ItemTech
    {
        public int ItemTechId { get; set; }

        public int ResumeCategoryItemId { get; set; }
        public ResumeCategoryItem ResumeCategoryItem { get; set; }

        public int TechIUsedId { get; set; }
        public TechIUsed TechIUsed { get; set; }
    }
}
