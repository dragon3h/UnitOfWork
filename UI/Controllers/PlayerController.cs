using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Mvc;
using UI.Utils;

namespace UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PlayerController> _logger;
    private readonly IMapper<Player, PlayerDTO> _mapper;

    public PlayerController(ILogger<PlayerController> logger, IUnitOfWork unitOfWork, IMapper<Player, PlayerDTO> mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var players = await _unitOfWork.Players.GetAll();
        if (players == null)
        {
            return NotFound();
        }

        var playersList = players.ToList();
        var playersListRequest = _mapper.MapList(playersList);

        return Ok(playersListRequest);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var player = await _unitOfWork.Players.GetById(id);
        if (player == null)
        {
            return NotFound();
        }

        var playerRequest = _mapper.Map(player);

        return Ok(playerRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PlayerDTO playerDto)
    {
        var player = _mapper.MapForCreation(playerDto);

        await _unitOfWork.Players.Add(player);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction("GetById", new { id = player!.Id }, player);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PlayerDTO playerToUpdate)
    {
        if (id == playerToUpdate.Id)
        {
            var player = await _unitOfWork.Players.GetById(id);
            if (player != null)
            {
                var mappedPlayer = _mapper.MapForUpdating(player, playerToUpdate);
                var updatedPlayer = await _unitOfWork.Players.Update(mappedPlayer);
                if (updatedPlayer == null)
                {
                    return BadRequest();
                }
            }

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // question: should i first check if the player exists before deleting it?
        var result = await _unitOfWork.Players.Delete(id);
        if (!result)
        {
            return BadRequest();
        }

        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}