using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FoodyDl.FoodyService;
using Foody_project.Models;
using FoodyDAL.Models;
using System;


namespace Foody_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharityController : ControllerBase
    {
        ICharity iCharity;
        public CharityController(ICharity Icharity) 
        {
         iCharity = Icharity;
        }
       
        // GET: api/Charity/2
        [HttpGet("{id:Guid}",Name ="CharityGet")]
        public ViewCharityModel Get(Guid id)
        {
            ViewCharityModel model = new()
            {
                Orders = iCharity.GetOrders(id),
           
                DelivaryMen = iCharity.GetDelivarymen()
            };
            return model ;
        }

        // POST api/Charity
        [HttpPost]
        public IActionResult Post([FromBody] OrderCharity orderCharity)
        {
            try
            {
                return Ok(iCharity.AddCharityOrder(orderCharity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //// PUT api/<Charity>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<Charity>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
