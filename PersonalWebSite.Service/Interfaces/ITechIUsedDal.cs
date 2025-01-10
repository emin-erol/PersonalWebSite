using PersonalWebSite.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Interfaces
{
    public interface ITechIUsedDal : IGenericDal<TechIUsed>
    {
        Task<int> GetTechIUsedIdByTechName(string name);
    }
}
