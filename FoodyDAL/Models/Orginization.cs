using System;
using System.Collections.Generic;

#nullable disable

namespace FoodyDAL.Models
{
    public partial class Orginization
    {
        public Orginization()
        {
            Branches = new HashSet<Branch>();
        }

        public Guid OrganizationId { get; set; }
        public string Org_name { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
    }
}
