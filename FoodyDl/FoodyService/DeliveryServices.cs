using FoodyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FoodyDl.FoodyService
{
    public interface IDelivary
    {
        public List<Order> GetOrders(Guid id);
   
        public bool AddDeliveryOrder(OrderDelivery orderDelivery);
    }
    public class DeliveryServices : IDelivary
    {
        readonly FoodyContext _context;
        public DeliveryServices(FoodyContext context)
        {
            _context = context;
        }

        public List<Order> GetOrders(Guid id)
        {
            if (id != Guid.Empty)
            {
                try
                {
                    List<Order> result = new List<Order>();
                    List<Order> orders = _context.Orders.Where(a => a.FkDeliveryId == id).ToList();
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
      

        public bool AddDeliveryOrder(OrderDelivery orderDelivery)
        {
            if (orderDelivery != null)
            {
                using (TransactionScope Scope = new())
                {
                    try
                    {
                        OrderDelivery order  = new()
                        {
                          FkOrderId = orderDelivery.FkOrderId,
                          FkIdDelivery = orderDelivery.FkIdDelivery,
                          DoneDelivery = orderDelivery.DoneDelivery
                        };
                        _context.OrderDeliveries.Add(order);
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
