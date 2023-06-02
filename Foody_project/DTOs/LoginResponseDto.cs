using FoodyDl.FoodyService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foody_project.DTOs
{
    public class LoginResponseDto
    {
        public bool ValidUser { get; set; }
        public UserDTO UserInfo { get; set; }

    }
}
