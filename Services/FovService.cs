using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using Fov.Models;
using System.Collections.Concurrent;

namespace Fov.Services;

public class FovService
{


    public static void SetPlayerFov(CCSPlayerController player, int fov)
    {
        if (player?.IsValid != true || player.PlayerPawn?.Value?.IsValid != true) return;
        player.DesiredFOV = (uint)fov;
        Utilities.SetStateChanged(player, "CBasePlayerController", "m_iDesiredFOV");
    }

    public static void SetPlayerFovWithAnimation(CCSPlayerController player, int targetFov, bool animate, 
        Config config, ConcurrentDictionary<ulong, int> playerFovs, ConcurrentDictionary<ulong, FovTransition> fovTransitions)
    {
        if (player?.IsValid != true || player.PlayerPawn?.Value?.IsValid != true) return;
        
        int currentFov = playerFovs.TryGetValue(player.SteamID, out int fov) ? fov : config.DefaultFov;
        
        bool useAnimation = config.EnableFovAnimation && animate;
        
        if (!useAnimation || Math.Abs(targetFov - currentFov) <= 3)
        {
            SetPlayerFov(player, targetFov);
            playerFovs[player.SteamID] = targetFov;
        }
        else
        {
            fovTransitions[player.SteamID] = new FovTransition
            {
                StartFov = currentFov,
                TargetFov = targetFov,
                Progress = 0.0f,
                Duration = config.FovAnimationDuration
            };
            
            playerFovs[player.SteamID] = targetFov;
        }
    }
} 