using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.Entities
{
    public class ResumeCategory
    {
        public int ResumeCategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ResumeCategoryItem> ResumeCategoryItems { get; set; }
        public string UserId { get; set; }
    }
}
