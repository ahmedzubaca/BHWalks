using AutoMapper;
using BHWalks.API.Models.Domain;
using BHWalks.API.Repositories;
using BHWalks.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepository _walksRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalksRepository walksRepository, IMapper mapper)
        {
            _walksRepository = walksRepository;
            _mapper = mapper;           
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walks = await _walksRepository.GetAllWalks();            
            var walksDto = _mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkById")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walk = await _walksRepository.GetWalkById(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDto = _mapper.Map<Models.DTO.Walk>(walk);         

            return Ok(walkDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalk(Models.DTO.addWalkRequest addWalkRequest)
        {
            //convert DTO to Domain object
            var walkDomain = new Models.Domain.Walk()
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            }; 
            
            //pass Domain object to WalkRepository
            var addedWalk = await _walksRepository.AddWalk(walkDomain); 

            //convert Domain back to DTO object
            if (addedWalk == null)
            {
                return BadRequest();
            }
            var walkDto = new Models.DTO.Walk()
            {
                Id= addedWalk.Id,
                Length = addedWalk.Length,
                Name = addedWalk.Name,
                RegionId = addedWalk.RegionId,
                WalkDifficultyId= addedWalk.WalkDifficultyId
            };

            //Send DTO object to client
            return CreatedAtAction(nameof(GetWalkById), new {id = walkDto.Id}, walkDto);            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            bool response = await _walksRepository.DeleteWalk(id);
            if (response == true)
            {
                return Ok("Walk deleted successfully");
            }
            return BadRequest();           
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk(Guid id, Models.DTO.addWalkRequest walkRequest)
        {
            //convert DTO to Domain object
            var walkDomain = new Models.Domain.Walk()
            {                
                Name = walkRequest.Name,
                Length = walkRequest.Length,
                RegionId = walkRequest.RegionId,
                WalkDifficultyId = walkRequest.WalkDifficultyId
            };

            //pass parametars to repository
            var updatedWalk = await _walksRepository.UpdateWalk(id, walkDomain);
            if(updatedWalk == null)
            {
                return NotFound();
            }
            //convert back from Domain to DTO object
            var walkDTO = new Models.DTO.Walk()
            {
                Id = updatedWalk.Id,
                Name = updatedWalk.Name,
                Length = updatedWalk.Length,
                RegionId = updatedWalk.RegionId,
                WalkDifficultyId = updatedWalk.WalkDifficultyId
            };

            //Return response
            return CreatedAtAction(nameof(GetWalkById), new {id=walkDTO.Id}, walkDTO);
        }
    }
}

