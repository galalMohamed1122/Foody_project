using AutoMapper;
using Foody_project.DTOs;
using Foody_project.Models;
using FoodyDAL.APIModels;
using FoodyDl.FoodyService;
using FoodyDl.FoodyService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foody_project.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurant _restaurantService;
        public RestaurantController( IRestaurant restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("{id:Guid}", Name = "Restaurant")]
        public ViewRestaurantModel Get(Guid id)
        {
            ViewRestaurantModel model = new()
            {
                Chairties = _restaurantService.GetChairties(),
                Locations = _restaurantService.GetLocations(id)
            };
            return model;

        }
        
        [HttpPost]
        public IActionResult Post([FromBody]RestaurantOrder orderModel)
        {
            try
            {
                return Ok(_restaurantService.InsertOrderByRestaurant(orderModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
