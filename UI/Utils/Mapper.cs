using Domain.Models;
using Infrastructure.DTOs;

namespace UI.Utils;

public class Mapper
{
    public List<PlayerRequest> MapPlayersToPlayerRequests(List<Player> players)
    {
        var playerRequests = new List<PlayerRequest>();
        foreach (var player in players)
        {
            var playerRequest = new PlayerRequest
            {
                Name = player.Name,
                Password = player.Password,
                Email = player.Email
            };
            playerRequests.Add(playerRequest);
        }

        return playerRequests;
    }
    
    public PlayerRequest MapPlayerToPlayerRequest(Player player)
    {
        return new PlayerRequest
        {
            Name = player.Name,
            Password = player.Password,
            Email = player.Email
        };
    }
    
    public Player MapPlayerRequestToPlayer(PlayerRequest playerRequest)
    {
        return new Player
        {
            Name = playerRequest.Name,
            Password = playerRequest.Password,
            Email = playerRequest.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = "System", // after implementing authentication, replace this with the authenticated user
            UpdatedBy = "System" // after implementing authentication, replace this with the authenticated user
        };
    }
}