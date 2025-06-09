using PersonalWebSite.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Interfaces
{
    public interface ISocialMediaDal : IGenericDal<SocialMedia>
    {
        Task<List<SocialMedia>> GetSocialMediasByUserId(string userId);
    }
}
