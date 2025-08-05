using CounterStrikeSharp.API.Core;
using System.Globalization;

namespace Fov.Services;

public static class LanguageDetectionService
{

    public static CultureInfo DetectPlayerLanguage(CCSPlayerController? player)
    {
        if (player?.IsValid != true) 
            return CultureInfo.DefaultThreadCurrentUICulture!;
        

        return new CultureInfo("tr-TR");
    }
} 