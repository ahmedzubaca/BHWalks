using BHWalks.API.Models.DTO;
using BHWalks.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace BHWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultiesRepository _walkDiffRepository;

        public WalkDifficultiesController(IWalkDifficultiesRepository walkDiffRepository)
        {
            _walkDiffRepository = walkDiffRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDiffs = await _walkDiffRepository.GetAllWalkDiff();
            var walkDiffsDTO = new List<Models.DTO.WalkDifficulty>();

            foreach(var walkDiff in walkDiffs)
            {
                var walkDiffDto = new Models.DTO.WalkDifficulty()
                {
                    Id = walkDiff.Id,
                    DifficultyCode = walkDiff.DifficultyCode,
                };
                walkDiffsDTO.Add(walkDiffDto);
            }           
            return Ok(walkDiffsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDiffById")]
        public async Task<IActionResult> GetWalkDiffById(Guid id)
        {
            var walkDiffDb = await _walkDiffRepository.GetWalkDiffById(id);
            if (walkDiffDb == null)
            {
                return BadRequest();
            }
            var walkDiffDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkDiffDb.Id,
                DifficultyCode = walkDiffDb.DifficultyCode
            };
            return Ok(walkDiffDTO);           
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkDiff(Models.DTO.AddWalkDiffRequest walkDiffRequest)
        {
            if(!ValidateWalkDifficultyModel(walkDiffRequest))
            {
                return BadRequest(ModelState);
            }
            var walkDiffDomain = new Models.Domain.WalkDifficulty()
            {
                DifficultyCode = walkDiffRequest.DifficultyCode
            };

            var addedWalkDiff = await _walkDiffRepository.AddWalkDiff(walkDiffDomain);

            var walkDiffDTO = new Models.DTO.WalkDifficulty()
            {
                Id = addedWalkDiff.Id,
                DifficultyCode = addedWalkDiff.DifficultyCode
            };
            return CreatedAtAction(nameof(GetWalkDiffById), new {id=walkDiffDTO.Id}, walkDiffDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkDiff(Guid id, Models.DTO.AddWalkDiffRequest addWalkDiff)
        {   
            if(!ValidateWalkDifficultyModel(addWalkDiff))
            {
                return BadRequest(ModelState);
            }
            var walkDiffDomain = new Models.Domain.WalkDifficulty()
            {
                DifficultyCode = addWalkDiff.DifficultyCode
            };

            var updatedWalkDiff = await _walkDiffRepository.UpdateWalkDiff(id, walkDiffDomain);
            if(updatedWalkDiff == null)
            {
                return NotFound();
            }
            var walkDiffDTO = new Models.DTO.WalkDifficulty()
            {
                Id = updatedWalkDiff.Id,
                DifficultyCode = updatedWalkDiff.DifficultyCode
            };
            return CreatedAtAction(nameof(GetWalkDiffById), new {id=walkDiffDTO.Id}, walkDiffDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkDiff(Guid id)
        {
            var deletedWalkDiff = await _walkDiffRepository.DeleteWalkDiff(id);
            if(deletedWalkDiff == null)
            {
                return NotFound();
            }
            var deletedDiffDTO = new Models.DTO.WalkDifficulty()
            {
                Id = deletedWalkDiff.Id,
                DifficultyCode = deletedWalkDiff.DifficultyCode
            };
            return CreatedAtAction(nameof(GetWalkDiffById), new {id=deletedDiffDTO.Id}, deletedDiffDTO);
        }

        #region Walk Difficulties Model Validation
        private bool ValidateWalkDifficultyModel(Models.DTO.AddWalkDiffRequest model)
        {
            if(model == null)
            {
                ModelState.AddModelError(nameof(model),
                    $"{nameof(model)}, cannot be empty!");
                return false;
            }
            if(string.IsNullOrWhiteSpace(model.DifficultyCode))
            {
                ModelState.AddModelError(nameof(model.DifficultyCode),
                    $"{nameof(model.DifficultyCode)} can not be emty!");
                return false;
            }
            return true;
        }
        #endregion
    }
}
