using PersonalWebSite.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Interfaces
{
    public interface IContactMailDal : IGenericDal<ContactMail>
    {
        Task MarkAsRead(int contactMailId);
        Task<int> GetNumberOfUnreadMails(string userId);
        Task RemoveBulk(List<int> contactMailIds);
        Task<List<ContactMail>> GetContactMailByUserId(string userId);
    }
}
