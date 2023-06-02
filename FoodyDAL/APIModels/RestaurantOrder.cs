using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodyDAL.APIModels
{
   public class RestaurantOrder
    {
        public Guid FkRestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantLocation { get; set; }
        public decimal? RestaurantPhone { get; set; }
        public Guid FkCharityId { get; set; }
        public string CharityName { get; set; }
        public DateTime? Time { get; set; }
        public string MealType { get; set; }
        public int? MealQty { get; set; }
        public DateTime? MealExpiry { get; set; }
        public string LeftoversType { get; set; }
        public int? LeftoversQty { get; set; }
        public DateTime? LeftoversExpiry { get; set; }
    }
}
