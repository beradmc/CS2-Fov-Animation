using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Core.Translations;
using System.Collections.Concurrent;
using Fov.Models;
using Fov.Services;
using Fov.Commands;
using Fov.Extensions;
using Microsoft.Extensions.Localization;

namespace Fov;

public class FovPlugin : BasePlugin, IPluginConfig<Config>
{
    public override string ModuleName => "FOV Plugin";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "beratfps";
    public override string ModuleDescription => "Custom FOV settings for players GitHub: https://github.com/beradmc/CS2-Fov-Animation";

    private readonly ConcurrentDictionary<ulong, int> _playerFovs = new();
    private readonly ConcurrentDictionary<ulong, FovTransition> _fovTransitions = new();
    public Config Config { get; set; } = new();

    private CounterStrikeSharp.API.Modules.Timers.Timer? _fovAnimationTimer;

    private string ChatPrefix => Config.Tag.ReplaceColorTags() + " ";

    public void OnConfigParsed(Config config)
    {
        config.DefaultFov = Math.Clamp(config.DefaultFov, config.FOVMin, config.FOVMax);
        config.FOVMin = Math.Max(60, config.FOVMin);
        config.FOVMax = Math.Min(130, config.FOVMax);
        config.FovAnimationDuration = Math.Clamp(config.FovAnimationDuration, 0.1f, 2.0f);
        
        Config = config;
        DatabaseService.InitializeConnection(Config);
    }

    public override void Load(bool hotReload)
    {
        DatabaseService.InitializeDatabase();
        
        RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn, HookMode.Pre);
        RegisterEventHandler<EventRoundStart>(OnRoundStart, HookMode.Pre);
        
        AddCommand("fov", "FOV değerini ayarla", (player, command) => 
            ChatCommands.ChatCommandFov(player, command, Config, _playerFovs, _fovTransitions, ChatPrefix, Localizer));
        AddCommand("fovreset", "FOV değerini sıfırla", (player, command) => 
            ChatCommands.ChatCommandFovReset(player, command, Config, _playerFovs, _fovTransitions, ChatPrefix, Localizer));

        _fovAnimationTimer = AddTimer(0.016f, () => AnimationService.AnimateFovTransitions(_fovTransitions), TimerFlags.REPEAT);
    }

    private HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
    {
        foreach (var player in Utilities.GetPlayers())
        {
            if (!player.IsValidPlayer()) continue;
            
            var savedFov = DatabaseService.LoadPlayerFov(player.SteamID);
            if (savedFov.HasValue)
            {
                FovService.SetPlayerFovWithAnimation(player, savedFov.Value, true, Config, _playerFovs, _fovTransitions);
                _playerFovs[player.SteamID] = savedFov.Value;
            }
        }
        return HookResult.Continue;
    }

    private HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (!player.IsValidPlayer()) 
            return HookResult.Continue;

        if (_playerFovs.TryGetValue(player.SteamID, out int cachedFov))
        {
            FovService.SetPlayerFovWithAnimation(player, cachedFov, true, Config, _playerFovs, _fovTransitions);
            return HookResult.Continue;
        }

        var savedFov = DatabaseService.LoadPlayerFov(player.SteamID);
        if (savedFov.HasValue)
        {
            FovService.SetPlayerFovWithAnimation(player, savedFov.Value, true, Config, _playerFovs, _fovTransitions);
            _playerFovs[player.SteamID] = savedFov.Value;
        }
        else
        {
            FovService.SetPlayerFovWithAnimation(player, Config.DefaultFov, true, Config, _playerFovs, _fovTransitions);
        }

        return HookResult.Continue;
    }

    [ConsoleCommand("css_fov", "Set your FOV")]
    [CommandHelper(minArgs: 0, usage: "<değer> [60-130 arası]")]
    public void CommandFov(CCSPlayerController? player, CommandInfo command)
    {
        ConsoleCommands.CommandFov(player, command, Config, _playerFovs, _fovTransitions, ChatPrefix, Localizer);
    }

    [ConsoleCommand("css_fovreset", "Reset your FOV to default")]
    public void CommandFovReset(CCSPlayerController? player, CommandInfo command)
    {
        ConsoleCommands.CommandFovReset(player, command, Config, _playerFovs, _fovTransitions, ChatPrefix, Localizer);
    }
}