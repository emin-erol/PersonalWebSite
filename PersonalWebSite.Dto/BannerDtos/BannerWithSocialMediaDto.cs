using PersonalWebSite.Dto.SocialMediaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Dto.BannerDtos
{
    public class BannerWithSocialMediaDto
    {
        public int BannerId { get; set; }
        public string? StartMessage { get; set; }
        public string? MeetMessage { get; set; }
        public string? Title { get; set; }
        public string? UserId { get; set; }
        public List<ResultSocialMediaDto> SocialMedias { get; set; }
    }
}
