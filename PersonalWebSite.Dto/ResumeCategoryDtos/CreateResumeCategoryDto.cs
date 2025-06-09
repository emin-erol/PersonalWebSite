using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Dto.ResumeCategoryDtos
{
	public class CreateResumeCategoryDto
	{
		public string CategoryName { get; set; }
        public string? UserId { get; set; }
    }
}
