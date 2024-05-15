using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PlayerController> _logger;

    public PlayerController(ILogger<PlayerController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var players = await _unitOfWork.Players.GetAll();
        if (players == null)
        {
            return NotFound();
        }

        return Ok(players);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var player = await _unitOfWork.Players.GetById(id);
        if (player == null)
        {
            return NotFound();
        }

        return Ok(player);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PlayerRequest playerRequest)
    {
        var player = new Player
        {
            Name = playerRequest.Name,
            Password = playerRequest.Password,
            Email = playerRequest.Email
        };
        await _unitOfWork.Players.Add(player);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction("GetById", new { id = player!.Id }, player);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Player? player)
    {
        if (id != player.Id)
        {
            return BadRequest();
        }

        var updatedPlayer = await _unitOfWork.Players.Update(player);
        if (updatedPlayer == null)
        {
            return BadRequest();
        }

        await _unitOfWork.CompleteAsync();
        return NoContent();
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