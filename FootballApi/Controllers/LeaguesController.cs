using AutoMapper;
using FootballApi.Core;
using FootballApi.Core.DTOs;
using FootballApi.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public LeaguesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
           var league = await _unitOfWork.Leagues.GetById(id);
            if (league is null)
                return NotFound();
            return Ok(league);
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync() 
        {
            var leagues = await _unitOfWork.Leagues.GetAllAsync();
            if (leagues is null)
                return NotFound();
            return Ok(leagues);
        }
        [HttpPost("AddOne")]
        public async Task<IActionResult> AddAsync(LeagueDTO model)
        {
            var newLeague = _mapper.Map<League>(model);
            await _unitOfWork.Leagues.Add(newLeague);
            await _unitOfWork.Complete();
            return Ok(newLeague);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(LeagueDTO model)
        {
            var league = await _unitOfWork.Leagues.FindAsync(l => l.Id == model.Id);
            if (league is null)
                return NotFound(model);

            league = _mapper.Map<League>(model);
            _unitOfWork.Leagues.Update(league);
            await _unitOfWork.Complete();
            return Ok(league);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var league = _unitOfWork.Leagues.Delete(id);
            if (league is null)
                return NotFound();

            await _unitOfWork.Complete();
            return Ok(league);
        }
    }
}
