using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nemo_v2_Api.Filters;
using Nemo_v2_Data.Entities;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserService _userService;
        private ILogger<UserController> _logger;
        public UserController(IUserService userService,
            ILogger<UserController> logger)
        {
            this._userService = userService;
            this._logger = logger;
        }

        [HttpPost("Add")]
        public IActionResult AddUser(string firstname,string lastname,string password,long restId)
        {
            try
            {
                var user = new User
                {
                    Firstame = firstname,
                    Lastname = lastname,
                    Password = password,
                    RestaurantId = restId,
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                var addedUser = _userService.InsertUser(user);
                _logger.LogInformation($"User Added");
                return Ok(addedUser);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        [Route("Update")]
        public IActionResult AddUser(long userId,string firstname,string lastname,string password,long restId)
        {
            try
            {
                var oldUser = _userService.GetUser(userId);
                oldUser.Firstame = firstname;
                oldUser.Lastname = lastname;
                oldUser.Password = password;
                oldUser.RestaurantId = restId;
                oldUser.ModifiedDate = DateTime.Now;
                var result =_userService.UpdateUser(oldUser);
                _logger.LogInformation($"User Updated : Firstname - {oldUser.Firstame}");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}