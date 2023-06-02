using FoodyDAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FoodyDl.FoodyService
{
    public interface ICharity
    {
        public List<Order> GetOrders(Guid id);
        public bool AddCharityOrder(OrderCharity orderCharity);
        public List<string[]> GetDelivarymen();
    }
    public class CharityServices : ICharity
    {
        readonly FoodyContext _context;
        public CharityServices(FoodyContext context) 
        {
            _context = context;
        }

        public List<Order> GetOrders(Guid id) 
        {
            if (id != Guid.Empty)
            {
                try
                {
                    List<Order> result = new();
                    List<Order> orders = _context.Orders.Where(a => a.FkCharityId == id).ToList();
                    foreach (var item in orders)
                    {
                        OrderDetail orderDetail = _context.OrderDetails.FirstOrDefault(o => o.Id == item.Id_order);
                        item.OrderDetails = new OrderDetail
                        {
                            Id = orderDetail.Id,
                            MealType = orderDetail.MealType,
                            MealQty = orderDetail.MealQty,
                            MealExpiry = orderDetail.MealExpiry,
                            LeftoversType = orderDetail.LeftoversType,
                            LeftoversQty = orderDetail.LeftoversQty,
                            LeftoversExpiry = orderDetail.LeftoversExpiry
                        };
                        result.Add(item);
                    }

                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            throw new Exception("Order id not found");
        }
      
        public List<string[]> GetDelivarymen()
        {
            List<string[]> results = new();
            var quary =
                from user in _context.Users
                where user.UserType == 3
                select new {user.UserId , user.FullName};
            foreach (var item in quary)
            {
                string[] row = new string[2];
                row[0] = item.UserId.ToString();
                row[1] = item.FullName;
                results.Add (row);
            }

            return results;
        }

        public bool AddCharityOrder(OrderCharity orderCharity)
        {
            if (orderCharity != null)
            {
                using (TransactionScope Scope = new())
                {
                    try
                    {
                        OrderCharity _orderCharity = new()
                        {
                            FkOrderId = orderCharity.FkOrderId,
                            FkCharityId = orderCharity.FkCharityId,
                            AcceptCharity = orderCharity.AcceptCharity,
                            DeliveryName = orderCharity.DeliveryName
                        };

                       var objId = _context.Users.Where(x => x.FullName == orderCharity.DeliveryName).Select(x => x.UserId).FirstOrDefault();
                        Order order = _context.Orders.FirstOrDefault(x => x.Id_order == orderCharity.FkOrderId);
                        if (order != null)
                        {
                            order.FkDeliveryId = objId;
                        }
                        _context.OrderCharities.Add(_orderCharity);
                        _context.Orders.Update(order);

                        _context.SaveChanges();

                        Scope.Complete();
                        Scope.Dispose();
                        return true;
                    }
                    catch (Exception)
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
