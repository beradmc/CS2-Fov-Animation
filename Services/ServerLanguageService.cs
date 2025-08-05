using System.Globalization;
using CounterStrikeSharp.API.Core.Translations;

namespace Fov.Services;

public static class ServerLanguageService
{
    private static CultureInfo? _serverLanguage;
    

    public static CultureInfo GetServerLanguage()
    {
        if (_serverLanguage != null)
            return _serverLanguage;
        
        try
        {
            var coreConfigPath = Path.Combine("addons", "counterstrikesharp", "configs", "core.json");
            
            if (File.Exists(coreConfigPath))
            {
                var jsonContent = File.ReadAllText(coreConfigPath);
                var config = System.Text.Json.JsonSerializer.Deserialize<CoreConfig>(jsonContent);
                
                if (config?.ServerLanguage != null)
                {
                    _serverLanguage = new CultureInfo(config.ServerLanguage);
                    return _serverLanguage;
                }
            }
        }
        catch
        {
        }
        
        _serverLanguage = new CultureInfo("tr-TR");
        return _serverLanguage;
    }
    

    public static void ClearCache()
    {
        _serverLanguage = null;
    }
}


public class CoreConfig
{
    public string? ServerLanguage { get; set; }
} 