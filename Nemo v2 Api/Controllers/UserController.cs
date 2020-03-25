using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nemo_v2_Api.Filters;
using Nemo_v2_Data;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Repo.Repositories;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private ILogger<UserController> _logger;
        private IMapper _mapper;
        public UserController(IUserService userService,
            ILogger<UserController> logger,
            IMapper mapper)
        {
            this._userService = userService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(long id)
        {
            try
            {
                var user = _userService.GetUser(id);
                if (user == null) throw new NullReferenceException("User Not Found");
                var userDto = _mapper.Map<UserDto>(user);
                _logger.LogInformation($"User Get {user.Id}");
                return Ok(userDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpGet("RestId/{RestaurantId}")]
        public IActionResult GetUserByRestaurantId(long RestaurantId)
        {
            try
            {
                var users = _userService.GetUsersByRestaurantId(RestaurantId);
                if (users == null) throw new NullReferenceException("User Not Found");
                var usersDtos =_mapper.Map<List<User>,List<UserDto>>(users.ToList());
                _logger.LogInformation($"Users Get By Restaurant Id:{RestaurantId}");
                return Ok(usersDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpPost("Add")]
        public IActionResult AddUser([FromBody]UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var addedUser = _userService.InsertUser(user);
                _logger.LogInformation($"User Added {user.Id}");
                return Ok(_mapper.Map<UserDto>(addedUser));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateUser([FromBody]UserDto userDto)
        {
            try
            {
                var updateUser = _mapper.Map<User>(userDto);
                var result =_userService.UpdateUser(updateUser);
                _logger.LogInformation($"User Updated : Firstname - {updateUser.Firstname}");
                return Ok(_mapper.Map<UserDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}