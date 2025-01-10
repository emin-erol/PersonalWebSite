using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Dto.ResumeCategoryItemDtos
{
    public class UpdateResumeCategoryItemDto
    {
        public int ResumeCategoryItemId { get; set; }
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? IconUrl { get; set; }
        public string? Header { get; set; }
        public string? Description { get; set; }
        public int ResumeCategoryId { get; set; }
        public List<string> TechNames { get; set; }
    }
}
