using AutoMapper;
using FoodyDAL.APIModels;

using FoodyDAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FoodyDl.FoodyService
{
    public interface IRestaurant
    {
        public List<string[]> GetChairties();
        public List<string> GetLocations(Guid id);
        public bool InsertOrderByRestaurant(RestaurantOrder orderModel);
    }

    public class RestaurantService :IRestaurant
    {
        private readonly FoodyContext _context;
        public RestaurantService(FoodyContext context)
        {
            _context = context;
        }


        public List<string[]> GetChairties()
        {
            List<string[]> results = new();
            var quary =
                from user in _context.Users
                where user.UserType == 2
                select new { user.UserId, user.FullName };
            foreach (var item in quary)
            {
                string[] row = new string[2];
                row[0] = item.UserId.ToString();
                row[1] = item.FullName;
                results.Add(row);
            }
            return results;
        }
        public List<string> GetLocations( Guid id)
        {
            List<string> Locations = _context.Branch
                                    .Where(x => x.OrganizationId == id)
                                    .Select(L => L.Location).ToList();
            return Locations;
        }
        public bool InsertOrderByRestaurant(RestaurantOrder orderModel)
        {
            if (orderModel != null)
            {
               
                using (TransactionScope Scope = new ())
                {
                    try
                    {
                        Order order = new()
                        {
                            FkRestaurantId = orderModel.FkRestaurantId,
                            RestaurantName = orderModel.RestaurantName,
                            RestaurantLocation = orderModel.RestaurantLocation,
                            RestaurantPhone = orderModel.RestaurantPhone,
                            FkCharityId = orderModel.FkCharityId,
                            CharityName = orderModel.CharityName,
                            Time = orderModel.Time,
                            OrderDetails = new OrderDetail()
                            {

                                MealType = orderModel.MealType,
                                MealQty = orderModel.MealQty,
                                MealExpiry = orderModel.MealExpiry,
                                LeftoversType = orderModel.LeftoversType,
                                LeftoversQty = orderModel.LeftoversQty,
                                LeftoversExpiry = orderModel.LeftoversExpiry
                            }
                        };
                        _context.Orders.Add(order);
                        int roweffected = _context.SaveChanges();
                        
                        Scope.Complete();
                        Scope.Dispose();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Scope.Dispose();
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
