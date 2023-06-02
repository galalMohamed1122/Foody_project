using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodyDl.FoodyService.DTOs
{
    public class LoginResponse
    {
        public bool ValidUser { get; set; }
        public UserDTO UserInfo { get; set; }
    }
}
