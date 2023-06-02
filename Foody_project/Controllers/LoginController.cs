using AutoMapper;
using Foody_project.DTOs;
using Foody_project.Models;
using FoodyDl.FoodyService;
using FoodyDl.FoodyService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Foody_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
         readonly IMapper _mapper;
         readonly IUser _userService;

        public LoginController(IUser foodyService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = foodyService;
        }

        // GET: api/<LoginController>

        [HttpPost(Name = "Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginReqModel _model)
        {
            LoginResponseDto Response;

            if (ModelState.IsValid)
            {
                LoginResponse Res = _userService.LoginService(_mapper.Map<LoginRequest>(_model));

                Response = _mapper.Map<LoginResponseDto>(Res);
            }
            else
            {
                return BadRequest();
            }
            return Ok(Response);
        }
    }
}
