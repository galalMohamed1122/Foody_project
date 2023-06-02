using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FoodyDAL.Models
{
    public partial class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public string MealType { get; set; }
        public int? MealQty { get; set; }
        public DateTime? MealExpiry { get; set; }
        public string LeftoversType { get; set; }
        public int? LeftoversQty { get; set; }
        public DateTime? LeftoversExpiry { get; set; }

        public virtual Order FkIdOrderNavigation { get; set; }
    }
}
