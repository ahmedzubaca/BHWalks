using AutoMapper;
using BHWalks.API.Data;
using BHWalks.API.Models.Domain;
using BHWalks.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BHWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionsRepository _regionsRepository;
        private readonly IMapper _mapper;

        public RegionsController
            (IRegionsRepository regionsRepository,
            IMapper maper)
        {
            _regionsRepository = regionsRepository;
            _mapper = maper;
        }

        [HttpGet]        
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionsRepository.GetAll();

            //return DTO regions
            //var regionsDTO = new List<Models.DTO.Region>();

            //foreach (var region in regions)
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        RegionCode = region.RegionCode,
            //        Area = region.Area,
            //        Latitude = region.Latitude,
            //        Longitude = region.Longitude,
            //        Population = region.Population

            //    };
            //    regionsDTO.Add(regionDTO);
            //}

            //using AutoMapper
            var regionsDTO = _mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionById")]

        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await _regionsRepository.GetRegion(id);
            if(region == null)
            {
                return NotFound();
            }
            var regionDTO = _mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(Models.DTO.addRegionRequest requestedRegion)
        {
            //convert requestedRegion to Models.Domain.Region 
            var region = new Models.Domain.Region()
            {
                Name = requestedRegion.Name,
                RegionCode = requestedRegion.RegionCode,
                Area = requestedRegion.Area,
                Latitude = requestedRegion.Latitude,
                Longitude = requestedRegion.Longitude,
                Population = requestedRegion.Population
            };

            //getting Models.Domain.Region from RegionRepository including Id

            region = await _regionsRepository.AddRegion(region);

            //convert region into Models.DTO.Region
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                RegionCode = region.RegionCode,
                Area = region.Area,
                Latitude = region.Latitude,
                Longitude = region.Longitude,
                Population = region.Population
            };

            //return data to a client
            return CreatedAtAction(nameof(GetRegionById), new {id=regionDTO.Id}, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
           var region = await _regionsRepository.DeleteRegion(id);
           if(region == null)
            {
                return NotFound();
            }
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                RegionCode = region.RegionCode,
                Area = region.Area,
                Latitude = region.Latitude,
                Longitude = region.Longitude,
                Population = region.Population
            };
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion(Guid id, Models.DTO.addRegionRequest requestedRegion)
        {
            var regionDomain = new Models.Domain.Region()
            {                
                Name = requestedRegion.Name,
                RegionCode = requestedRegion.RegionCode,
                Area = requestedRegion.Area,
                Latitude = requestedRegion.Latitude,
                Longitude = requestedRegion.Longitude,
                Population = requestedRegion.Population
            };

            var updatetRegion = await _regionsRepository.UpdateRegion(id, regionDomain);
            if(updatetRegion == null)
            {
                return NotFound();
            }
            var regionDTO = new Models.DTO.Region()
            {
                Id = updatetRegion.Id,
                Name = updatetRegion.Name,
                RegionCode = updatetRegion.RegionCode,
                Area = updatetRegion.Area,
                Latitude = updatetRegion.Latitude,
                Longitude = updatetRegion.Longitude,
                Population = updatetRegion.Population
            };
            return Ok(regionDTO);
        }
    }
}
