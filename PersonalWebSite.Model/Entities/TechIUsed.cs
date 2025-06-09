using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.Entities
{
    public class TechIUsed
    {
        public int TechIUsedId { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public ICollection<ItemTech> ItemTeches { get; set; }
    }
}
