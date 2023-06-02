using System;
using System.Collections.Generic;

#nullable disable

namespace FoodyDAL.Models
{
    public partial class OrderDelivery
    {
        public int Id { get; set; }
        public Guid FkIdDelivery { get; set; }
        public int? FkOrderId { get; set; }
        public bool? DoneDelivery { get; set; }

        public virtual User FkDelivery { get; set; } 
        public virtual Order FkOrder { get; set; }
    }
}
