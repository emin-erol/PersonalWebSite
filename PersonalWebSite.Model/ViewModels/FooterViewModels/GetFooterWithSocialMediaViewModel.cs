using PersonalWebSite.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.ViewModels.FooterViewModels
{
	public class GetFooterWithSocialMediaViewModel
	{
        public int FooterId { get; set; }
        public string? Description { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
    }
}
