using PersonalWebSite.Dto.SocialMediaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Dto.FooterDtos
{
	public class FooterWithSocialMediaDto
	{
		public int FooterId { get; set; }
		public string? Description { get; set; }
        public ICollection<ResultSocialMediaDto> SocialMedias { get; set; }
    }
}
