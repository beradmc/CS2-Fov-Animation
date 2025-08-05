using CounterStrikeSharp.API.Core;

namespace Fov.Extensions;

public static class PlayerExtensions
{
    public static bool IsValidPlayer(this CCSPlayerController? player)
    {
        return player?.IsValid == true && player.PlayerPawn?.Value?.IsValid == true;
    }
} 