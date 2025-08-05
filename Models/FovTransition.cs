namespace Fov.Models;

public class FovTransition
{
    public int StartFov { get; set; }
    public int TargetFov { get; set; }
    public float Progress { get; set; } = 0.0f;
    public float Duration { get; set; }
} 