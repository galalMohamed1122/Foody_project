using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodyDl.FoodyService.DTOs
{
    public class LoginRequest
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
