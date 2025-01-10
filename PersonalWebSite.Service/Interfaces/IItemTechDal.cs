using PersonalWebSite.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Interfaces
{
    public interface IItemTechDal : IGenericDal<ItemTech>
    {
        Task<int> GetItemTechIdByResumeCategoryItemIdAndTechIUsedName(int resumeCategoryItemId, string name);
    }
}
