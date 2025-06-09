using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.ViewModels.SocialMediaViewModels
{
    public class CreateSocialMediaViewModel
    {
        public string Name { get; set; }
        public string? IconUrl { get; set; }
        public string? SocialMediaUrl { get; set; }
        public string? UserId { get; set; }
    }
}
