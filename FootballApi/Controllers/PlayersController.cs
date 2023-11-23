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
    public class PlayersController : ControllerBase
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public PlayersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var player = await _unitOfWork.Players.GetById(id);
            if (player is null)
                return NotFound();
            return Ok(player);
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var players = await _unitOfWork.Players.GetAllAsync();
            if (players is null)
                return NotFound();
            return Ok(players);
        }
        [HttpPost("AddOne")]
        public async Task<IActionResult> AddAsync(PlayerDTO model)
        {
            var newplayer = _mapper.Map<Player>(model);
            await _unitOfWork.Players.Add(newplayer);
            await _unitOfWork.Complete();
            return Ok(newplayer);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(PlayerDTO model)
        {
            var player = await _unitOfWork.Players.FindAsync(l => l.Id == model.Id);
            if (player is null)
                return NotFound(model);

            player = _mapper.Map<Player>(model);
            _unitOfWork.Players.Update(player);
            await _unitOfWork.Complete();
            return Ok(player);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var player = _unitOfWork.Players.Delete(id);
            if (player is null)
                return NotFound();

            await _unitOfWork.Complete();
            return Ok(player);
        }
    }
}

