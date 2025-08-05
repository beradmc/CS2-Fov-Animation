using CounterStrikeSharp.API.Modules.Config;
using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace Fov.Models;

public class Config : BasePluginConfig
{
    // ===== GENERAL SETTINGS =====
    [JsonPropertyName("DefaultFov")]
    public int DefaultFov { get; set; } = 90; // Original FOV value

    [JsonPropertyName("Tag")]
    public string Tag { get; set; } = "{GOLD}[FOV]{DEFAULT}"; // Chat message prefix

    // ===== DATABASE SETTINGS =====
    [JsonPropertyName("DatabaseHost")]
    public string DatabaseHost { get; set; } = "localhost"; // MySQL server host

    [JsonPropertyName("DatabaseName")]
    public string DatabaseName { get; set; } = "fovdb"; // Database name

    [JsonPropertyName("DatabaseUser")]
    public string DatabaseUser { get; set; } = "fovuser"; // Database username

    [JsonPropertyName("DatabasePassword")]
    public string DatabasePassword { get; set; } = "password"; // Database password

    // ===== FOV LIMITS =====
    [JsonPropertyName("FOVMin")]
    public int FOVMin { get; set; } = 60; // Minimum FOV value

    [JsonPropertyName("FOVMax")]
    public int FOVMax { get; set; } = 130; // Maximum FOV value

    // ===== ANIMATION SETTINGS =====
    [JsonPropertyName("EnableFovAnimation")]
    public bool EnableFovAnimation { get; set; } = true; // Enable FOV animation

    [JsonPropertyName("FovAnimationDuration")]
    public float FovAnimationDuration { get; set; } = 0.3f; // Animation duration in seconds
} 