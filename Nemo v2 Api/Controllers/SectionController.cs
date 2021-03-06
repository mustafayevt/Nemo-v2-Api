﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nemo_v2_Api.Filters;
using Nemo_v2_Data;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Helper;
using Nemo_v2_Service.Abstraction;

namespace Nemo_v2_Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SectionController : Controller
    {
        private readonly ISectionService _sectionService;
        private readonly ILogger<SectionController> _logger;
        private readonly IMapper _mapper;

        public SectionController(ISectionService sectionService,
            ILogger<SectionController> logger,
            IMapper mapper)
        {
            this._sectionService = sectionService;
            this._logger = logger;
            this._mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSection(long id)
        {
            try
            {
                var Section = _sectionService.GetSection(id);
                if (Section == null) throw new NullReferenceException("Section Not Found");
                var SectionDto = _mapper.Map<SectionDto>(Section);
                _logger.LogInformation($"Section Get {Section.Id}");
                return Ok(SectionDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpGet("{RestaurantId}")]
        public async Task<IActionResult> GetSectionByRestaurantId(long RestaurantId)
        {
            try
            {
                var Sections = _sectionService.GetSectionsByRestaurantId(RestaurantId);
                if (Sections == null) throw new NullReferenceException("Section Not Found");
                var SectionsDtos = _mapper.Map<List<Section>, List<SectionDto>>(Sections.ToList());
                _logger.LogInformation($"Sections Get By Restaurant Id:{RestaurantId}");
                return Ok(SectionsDtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSection([FromBody] SectionDto SectionDto)
        {
            try
            {
                var Section = _mapper.Map<Section>(SectionDto);
                var addedSection = _sectionService.InsertSection(Section);
                _logger.LogInformation($"Section Added {Section.Id}");
                return Ok(_mapper.Map<SectionDto>(addedSection));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return BadRequest(e.GetAllMessages());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSection([FromBody] SectionDto SectionDto)
        {
            try
            {
                var updateSection = _mapper.Map<Section>(SectionDto);
                var result = _sectionService.UpdateSection(updateSection);
                _logger.LogInformation($"Section Updated : Id: {updateSection.Id}");
                return Ok(_mapper.Map<SectionDto>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetAllMessages());
                return NotFound(e.GetAllMessages());
            }
        }
    }
}