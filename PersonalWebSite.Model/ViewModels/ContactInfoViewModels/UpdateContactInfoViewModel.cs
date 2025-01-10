using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.ViewModels.ContactInfoViewModels
{
	public class UpdateContactInfoViewModel
	{
		public int ContactInfoId { get; set; }
		public string? IconUrl { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
	}
}
