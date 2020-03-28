using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nemo_v2_Api.Filters;
using Nemo_v2_Data;
using Nemo_v2_Data.Entities;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;
        private ILogger<RoleController> _logger;
        private IMapper _mapper;
        public RoleController(IRoleService roleService,
            ILogger<RoleController> logger,
            IMapper mapper)
        {
            this._roleService = roleService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetRole(long id)
        {
            try
            {
                var role = _roleService.GetRole(id);
                if (role == null) throw new NullReferenceException("Role Not Found");
                var roleDto = _mapper.Map<RoleDto>(role);
                _logger.LogInformation($"User Get {role.Id}");
                return Ok(roleDto);
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
                var roles = _roleService.GetRolesByRestaurantId(RestaurantId);
                if (roles == null) throw new NullReferenceException("Role Not Found");
                var rolesDto =_mapper.Map<List<Role>,List<RoleDto>>(roles.ToList());
                _logger.LogInformation($"Roles Get By Restaurant Id:{RestaurantId}");
                return Ok(rolesDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
        
        [HttpPost]
        public IActionResult AddRole([FromBody]RoleDto roleDto)
        {
            try
            {
                var role = _mapper.Map<Role>(roleDto);
                var addedRole = _roleService.InsertRole(role);
                _logger.LogInformation($"Role Added {role.Id}");
                return Ok(_mapper.Map<RoleDto>(addedRole));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        public IActionResult UpdateRole([FromBody]RoleDto roleDto)
        {
            try
            {
                var updatedRole = _mapper.Map<Role>(roleDto);
                var result =_roleService.UpdateRole(updatedRole);
                _logger.LogInformation($"Role Updated : {updatedRole.Name}");
                return Ok(_mapper.Map<RoleDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}