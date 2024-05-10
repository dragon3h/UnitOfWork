using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Models;
using UnitOfWork.Services.IRepositories;

namespace UnitOfWork.Controllers;

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
    public async Task<IActionResult> Add(Player player)
    {
        if (ModelState.IsValid)
        {
            var result = await _unitOfWork.Players.Add(player);
            if (!result)
            {
                return BadRequest();
            }

            await _unitOfWork.CompleteAsync();
            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }
        
        return new JsonResult("Something went wrong"){StatusCode = 500};
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Update(int id, Player player)
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