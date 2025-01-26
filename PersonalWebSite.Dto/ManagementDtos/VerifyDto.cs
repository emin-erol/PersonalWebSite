using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Dto.ManagementDtos
{
    public class VerifyDto
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }
}
