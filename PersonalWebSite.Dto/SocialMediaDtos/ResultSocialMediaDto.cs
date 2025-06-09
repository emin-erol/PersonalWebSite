using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Dto.SocialMediaDtos
{
    public class ResultSocialMediaDto
    {
        public int SocialMediaId { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string SocialMediaUrl { get; set; }
        public string UserId { get; set; }
    }
}
