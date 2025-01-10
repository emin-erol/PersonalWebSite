using PersonalWebSite.Dto.ResumeCategoryItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Dto.ResumeCategoryDtos
{
    public class ResultResumeCategoryDto
    {
        public int ResumeCategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ResultResumeCategoryItemDto> ResumeCategoryItems { get; set; }
    }
}
