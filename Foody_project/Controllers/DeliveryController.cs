using Foody_project.Models;
using FoodyDAL.Models;
using FoodyDl.FoodyService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Foody_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        IDelivary iDelivary;
        public DeliveryController(IDelivary Idelivery)
        {
            iDelivary = Idelivery;  
        }


        // GET api/Delivery/3
        [HttpGet("{id:Guid}",Name = "DeliveryGet")]

        public ViewDeliveryModel Get(Guid id)
        {
            ViewDeliveryModel model = new ViewDeliveryModel
            {
                Orders = iDelivary.GetOrders(id),
               
            };
            return model;
        }

        // POST api/Delivery
        [HttpPost]
        public IActionResult Post([FromBody] OrderDelivery orderDelivery)
        {
            try
            {
                return Ok(iDelivary.AddDeliveryOrder(orderDelivery));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<DeliveryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<DeliveryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
