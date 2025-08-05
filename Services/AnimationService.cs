using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Fov.Models;
using Fov.Services;
using System.Collections.Concurrent;

namespace Fov.Services;

public class AnimationService
{
    public static void AnimateFovTransitions(ConcurrentDictionary<ulong, FovTransition> fovTransitions)
    {
        if (fovTransitions.Count == 0) return;
        
        var completedTransitions = new List<ulong>(fovTransitions.Count);
        
        foreach (var kvp in fovTransitions)
        {
            ulong steamId = kvp.Key;
            FovTransition transition = kvp.Value;
            
            transition.Progress += 0.016f / transition.Duration;
            
            if (transition.Progress >= 1.0f)
            {
                var player = Utilities.GetPlayers().FirstOrDefault(p => p.SteamID == steamId);
                if (player?.IsValid == true && player.PlayerPawn?.Value?.IsValid == true)
                {
                    FovService.SetPlayerFov(player, transition.TargetFov);
                }
                completedTransitions.Add(steamId);
            }
            else
            {
                int currentFov = (int)Math.Round(transition.StartFov + (transition.TargetFov - transition.StartFov) * transition.Progress);
                var player = Utilities.GetPlayers().FirstOrDefault(p => p.SteamID == steamId);
                if (player?.IsValid == true && player.PlayerPawn?.Value?.IsValid == true)
                {
                    FovService.SetPlayerFov(player, currentFov);
                }
            }
        }
        
        foreach (var steamId in completedTransitions)
        {
            fovTransitions.TryRemove(steamId, out _);
        }
    }
} 