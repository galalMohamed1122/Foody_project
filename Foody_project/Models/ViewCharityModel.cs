using FoodyDAL.Models;
using System.Collections.Generic;


namespace Foody_project.Models
{
    public class ViewCharityModel
    {
        public IEnumerable<Order> Orders { get; set; }
      
        public List<string[]> DelivaryMen { get; set; }

    }
}
