using Microsoft.Extensions.Localization;
using CounterStrikeSharp.API.Core.Translations;
using CounterStrikeSharp.API.Core;
using System.Globalization;
using Fov.Services;

namespace Fov.Extensions;

public static class LocalizerExtensions
{

    public static string ForPlayer(this IStringLocalizer localizer, CCSPlayerController? player, string key)
    {
        var playerLanguage = player.GetLanguage();
        
        if (playerLanguage.Equals(CultureInfo.DefaultThreadCurrentUICulture))
        {
            playerLanguage = ServerLanguageService.GetServerLanguage();
        }
        
        using WithTemporaryCulture temporaryCulture = new WithTemporaryCulture(playerLanguage);
        return localizer[key];
    }


    public static string ForPlayer(this IStringLocalizer localizer, CCSPlayerController? player, string key, params object[] args)
    {
        var playerLanguage = player.GetLanguage();
        
        if (playerLanguage.Equals(CultureInfo.DefaultThreadCurrentUICulture))
        {
            playerLanguage = ServerLanguageService.GetServerLanguage();
        }
        
        using WithTemporaryCulture temporaryCulture = new WithTemporaryCulture(playerLanguage);
        return localizer[key, args];
    }
} 