using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodyDl.FoodyService.DTOs
{
    public class UserDTO
    {
        public string EMail { get; set; }
        public string FullName { get; set; }
        public decimal? Phone { get; set; }
        public string UserType { get; set; }
        public string OrgName { get; set; }
        public string OrgType { get; set; }
        public int IdUsers { get; set; }
    }
}
