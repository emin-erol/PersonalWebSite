using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Dto.ManagementDtos
{
    public class UserLoginResponseDto
    {
        public string Message { get; set; }
        public UserDto User { get; set; }
    }
}
