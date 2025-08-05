# FOV Plugin

<p align="center">
  <img src="https://github.com/beradmc/CS2-Fov-Animation/raw/main/gif/fov.gif" width="400" alt="FOV Animation Demo 1"> <img src="https://github.com/beradmc/CS2-Fov-Animation/raw/main/gif/fov1.gif" width="400" alt="FOV Animation Demo 2">
</p>

## What Does It Do?

This plugin allows players to customize their FOV (Field of View) settings on your Counter-Strike 2 server. Players can set FOV values between 60-130 degrees.

## Features

* ✅ Works automatically (no commands needed)
* ✅ Both chat and console commands
* ✅ Smooth FOV transition animations
* ✅ Persistent storage in database
* ✅ English and Turkish language support

## Installation

1. Copy the `Fov.dll` file to your server's plugins folder:  
```  
csgo/addons/counterstrikesharp/plugins/Fov/  
```
2. Restart your server
3. The plugin will start working automatically

## Configuration

When the plugin first runs, a config file is created at this location:
```
csgo/addons/counterstrikesharp/configs/plugins/Fov/Fov.json
```

**Example Config:**
```json
{
  "DefaultFov": 90,                    // Default FOV value for new players
  "Tag": "{GOLD}[FOV]{DEFAULT}",       // Chat message tag
  "DatabaseHost": "localhost",          // Database server address
  "DatabaseName": "fovdb",             // Database name
  "DatabaseUser": "fovuser",           // Database username
  "DatabasePassword": "password",       // Database password
  "FOVMin": 60,                        // Minimum FOV value
  "FOVMax": 130,                       // Maximum FOV value
  "EnableFovAnimation": true,          // Enable FOV animations
  "FovAnimationDuration": 0.3          // Animation duration (seconds)
}
```

## Commands

**Chat Commands:**
- `!fov <value>` - Set your FOV (60-130 range)
- `!fovreset` - Reset FOV to default value

## Requirements

* [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp)

---