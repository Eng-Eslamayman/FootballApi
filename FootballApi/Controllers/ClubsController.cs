using AutoMapper;
using FootballApi.Core.DTOs;
using FootballApi.Core.Models;
using FootballApi.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public ClubsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var club = await _unitOfWork.Clubs.GetById(id);
            if (club is null)
                return NotFound();
            return Ok(club);
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var clubs = await _unitOfWork.Clubs.GetAllAsync();
            if (clubs is null)
                return NotFound();
            return Ok(clubs);
        }
        [HttpPost("AddOne")]
        public async Task<IActionResult> AddAsync(ClubDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var newclub = _mapper.Map<Club>(model);
            await _unitOfWork.Clubs.Add(newclub);
            await _unitOfWork.Complete();
            return Ok(newclub);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ClubDTO model)
        {
            var club = await _unitOfWork.Clubs.FindAsync(l => l.Id == model.Id);
            if (club is null)
                return NotFound(model);

            club = _mapper.Map<Club>(model);
            _unitOfWork.Clubs.Update(club);
            await _unitOfWork.Complete();
            return Ok(club);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
           var club = _unitOfWork.Clubs.Delete(id);
            if (club is null)
                return NotFound();

            await _unitOfWork.Complete();
            return Ok(club);
        }
    }
}

