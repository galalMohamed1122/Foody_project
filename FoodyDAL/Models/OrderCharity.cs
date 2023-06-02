using System;
using System.Collections.Generic;

#nullable disable

namespace FoodyDAL.Models
{
    public partial class OrderCharity
    {
        public int Id { get; set; }
        public int? FkOrderId { get; set; }
        public Guid FkCharityId { get; set; }
        public bool? AcceptCharity { get; set; }
        public string DeliveryName { get; set; }

        public virtual User FkCharity { get; set; }
        public virtual Order FkOrder { get; set; }
    }
}
