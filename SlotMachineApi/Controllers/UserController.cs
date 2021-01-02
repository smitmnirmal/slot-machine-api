using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlotMachineApi.Models;
using SlotMachineApi.Services;

namespace SlotMachineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult Authenticate([FromForm] User user)
        {
            User userInfo = _userService.Get(user.UserName);

            if (userInfo != null && (userInfo.Password == user.Password))
            {
                /*return Ok("{\"success\": True}");*/
                return Ok("Success");
            }

            return NotFound();
        }

    }
}