using AutoMapper;
using Foody_project.DTOs;
using Foody_project.Models;
using FoodyDAL.APIModels;
using FoodyDAL.Models;
using FoodyDl.FoodyService;
using FoodyDl.FoodyService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
namespace Foody_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        
        private readonly IUser _userService;

        public UserController(IUser foodyService, IMapper mapper)
        {
            _userService = foodyService;
        }
       


    // Users

    [HttpGet(Name = "GetAllUsers")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            try
            {
                return Ok(_userService.GetUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:Guid}", Name = "GetUserById")]
        public ActionResult<User> GetUserById(Guid id)
        {
            try
            {
                return _userService.GetUserById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost(Name = "CreateUser")]
        public IActionResult CreateUser([FromBody] NewUserModel user)
        {
            try
            {
                return Ok(_userService.InsertUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut( Name = "UpdateUser")]
        public IActionResult UpdateUser([FromBody] UbdateUserModel user)
        {
            try
            {
                return Ok(_userService.UpdatetUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:Guid}", Name = "DeleteUser")]
        public IActionResult DeletetUser(Guid id)
        {
            try
            {
                return Ok(_userService.DeletetUser(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
