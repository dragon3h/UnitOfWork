using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.DTOs;

namespace UI.Utils;

public class PlayerMapper : IMapper<Player, PlayerDTO>
{
    public List<PlayerDTO> MapList(List<Player> players)
    {
        var playerListResponse = new List<PlayerDTO>();

        players.ForEach(player =>
        {
            var playerRequest = new PlayerDTO
            {
                Id = player.Id,
                Name = player.Name,
                Password = player.Password,
                Email = player.Email
            };
            playerListResponse.Add(playerRequest);
        });

        return playerListResponse;
    }

    public PlayerDTO Map(Player player)
    {
        return new PlayerDTO
        {
            Id = player.Id,
            Name = player.Name,
            Password = player.Password,
            Email = player.Email
        };
    }

    public Player MapForCreation(PlayerDTO playerDto)
    {
        return new Player
        {
            Name = playerDto.Name,
            Password = playerDto.Password,
            Email = playerDto.Email,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "System", // after implementing authentication, replace this with the authenticated user
        };
    }

    public Player MapForUpdating(Player player, PlayerDTO playerToUpdate)
    {
        player.Name = playerToUpdate.Name;
        player.Password = playerToUpdate.Password;
        player.Email = playerToUpdate.Email;
        player.UpdatedAt = DateTime.UtcNow;
        player.UpdatedBy = "System"; // after implementing authentication, replace this with the authenticated user

        return player;
    }
}