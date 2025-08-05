using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Core.Translations;
using Fov.Models;
using Fov.Services;
using Fov.Extensions;
using System.Collections.Concurrent;
using Microsoft.Extensions.Localization;

namespace Fov.Commands;

public class ChatCommands
{
    public static void ChatCommandFov(CCSPlayerController? player, CommandInfo command, 
        Config config, ConcurrentDictionary<ulong, int> playerFovs, ConcurrentDictionary<ulong, FovTransition> fovTransitions, 
        string chatPrefix, IStringLocalizer localizer)
    {
        if (player?.IsValid != true || player.PlayerPawn?.Value?.IsValid != true) return;
        
        int currentFov = playerFovs.TryGetValue(player.SteamID, out int fov) ? fov : config.DefaultFov;
        
        if (command.ArgCount == 1)
        {
            string message = $"{chatPrefix}{localizer.ForPlayer(player, "fov.current", currentFov)}\n" +
                             $"{chatPrefix}{localizer.ForPlayer(player, "fov.usage", config.FOVMin, config.FOVMax)}\n" +
                             $"{chatPrefix}{localizer.ForPlayer(player, "fov.reset.usage")}";
            player.PrintToChat(message);
            return;
        }
        

        if (!int.TryParse(command.ArgByIndex(1), out int newFov))
        {
            player.PrintToChat($"{chatPrefix}{localizer.ForPlayer(player, "fov.invalid", config.FOVMin, config.FOVMax)}");
            return;
        }

        if (newFov < config.FOVMin || newFov > config.FOVMax)
        {
            player.PrintToChat($"{chatPrefix}{localizer.ForPlayer(player, "fov.invalid", config.FOVMin, config.FOVMax)}");
            return;
        }

        FovService.SetPlayerFovWithAnimation(player, newFov, true, config, playerFovs, fovTransitions);
        playerFovs[player.SteamID] = newFov;
        DatabaseService.SavePlayerFov(player.SteamID, newFov);
        
        player.PrintToChat($"{chatPrefix}{localizer.ForPlayer(player, "fov.changed", newFov)}");
    }

    public static void ChatCommandFovReset(CCSPlayerController? player, CommandInfo command, 
        Config config, ConcurrentDictionary<ulong, int> playerFovs, ConcurrentDictionary<ulong, FovTransition> fovTransitions, 
        string chatPrefix, IStringLocalizer localizer)
    {
        if (player?.IsValid != true || player.PlayerPawn?.Value?.IsValid != true) return;
        
        FovService.SetPlayerFovWithAnimation(player, config.DefaultFov, true, config, playerFovs, fovTransitions);
        playerFovs[player.SteamID] = config.DefaultFov;
        DatabaseService.SavePlayerFov(player.SteamID, config.DefaultFov);
        
        player.PrintToChat($"{chatPrefix}{localizer.ForPlayer(player, "fov.reset", config.DefaultFov)}");
    }
} 