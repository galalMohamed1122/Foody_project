using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodyDAL.Models
{
    public partial class Branch
    {
        public Branch() 
        {
         Users = new HashSet<User>();
        }
        public Guid BranchId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Location { get; set; }

        public virtual Orginization Orginization { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
