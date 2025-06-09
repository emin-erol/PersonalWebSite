using PersonalWebSite.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.ViewModels.BannerViewModels
{
    public class CreateBannerViewModel
    {
        public string? StartMessage { get; set; }
        public string? MeetMessage { get; set; }
        public string? Title { get; set; }
        public string UserId { get; set; }
    }
}
