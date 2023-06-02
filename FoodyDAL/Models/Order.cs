using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FoodyDAL.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderCharities = new HashSet<OrderCharity>();
            OrderDeliveries = new HashSet<OrderDelivery>();
            OrderDetails = new OrderDetail();
        }
        [Key]
        public int Id_order { get; set; }
        public Guid FkRestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantLocation { get; set; }
        public decimal? RestaurantPhone { get; set; }
        public Guid FkCharityId { get; set; }
        public string CharityName { get; set; }
        public Guid FkDeliveryId { get; set; }

        public DateTime? Time { get; set; }

        public virtual OrderDetail OrderDetails { get; set; }
        public virtual User FkCharity { get; set; }
        public virtual User FkRestaurant { get; set; }
        public virtual ICollection<OrderCharity> OrderCharities { get; set; }
        public virtual ICollection<OrderDelivery> OrderDeliveries { get; set; }
    }
}
