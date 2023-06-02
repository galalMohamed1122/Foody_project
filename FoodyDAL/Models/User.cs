using System;
using System.Collections.Generic;

#nullable disable

namespace FoodyDAL.Models
{
    public partial class User
    {
        public User()
        {
            OrderCharityHistory = new HashSet<OrderCharity>();
            OrderDeliveyHistory = new HashSet<OrderDelivery>();
            OrderFkCharities = new HashSet<Order>();
            OrderFkRestaurants = new HashSet<Order>();
        }

        public Guid UserId { get; set; }
        public string EMail { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public decimal? Phone { get; set; }
        public int UserType { get; set; }
        public Guid BranchId { get; set; }

       public virtual Branch Branch { get; set; }

        public virtual ICollection<Order> OrderFkCharities { get; set; }
        public virtual ICollection<Order> OrderFkRestaurants { get; set; }
        public virtual ICollection<OrderCharity> OrderCharityHistory { get; set; }
        public virtual ICollection<OrderDelivery> OrderDeliveyHistory { get; set; }

    }
}
