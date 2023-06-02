using FoodyDAL.APIModels;
using FoodyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace FoodyDAL.FoodyRepository
{
    public class RestaurantRepository
    {
        private readonly FoodyContext _context;
        public RestaurantRepository(FoodyContext context)
        {
            _context = context;
        }
        public List<ViewChairty> GetChairty()
        {
            return _context.ViewChairties.ToList();
        }
        public List<string> GetLocation(string type, string name)
        {   
            List<string> Locations = _context.Orginizations
                                    .Where(x => x.OrgType == type && x.OrgName == name)
                                    .Select(L => L.OrgLocation)
                                    .ToList();
            return Locations;
        }
        public InsertOrderByRestaurant InsertOrderByRestaurant(InsertOrderByRestaurant orderModel)
        {
            if (orderModel != null)
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    try
                    {
                        Order order = new Order()
                        {
                            RestaurantName = orderModel.RestaurantName,
                            RestaurantLocation = orderModel.RestaurantLocation,
                            RestaurantPhone = orderModel.RestaurantPhone,
                            CharityName = orderModel.CharityName,
                            Time = orderModel.Time
                        };
                        _context.Orders.Add(order);
                        int roweffected = _context.SaveChanges();
                        if (roweffected > 0)
                        {
                            OrderDetail details = new OrderDetail()
                            {
                                MealType = orderModel.MealType,
                                MealQty = orderModel.MealQty,
                                MealExpiry = orderModel.MealExpiry,
                                LeftoversType = orderModel.LeftoversType,
                                LeftoversQty = orderModel.LeftoversQty,
                                LeftoversExpiry = orderModel.LeftoversExpiry
                            };
                            _context.OrderDetails.Add(details);
                            roweffected = _context.SaveChanges();
                            orderModel.IdOrder = order.Id_order;
                        }
                        Scope.Complete();
                        Scope.Dispose();
                        return orderModel;
                    }
                    catch (Exception e)
                    {
                        Scope.Dispose();
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
    
       
   
