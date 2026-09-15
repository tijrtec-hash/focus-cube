namespace FocusCube;

public sealed class FocusCubeSettings
{
    public double? Left { get; set; }
    public double? Top { get; set; }
    public bool DrawerExpanded { get; set; } = true;
    public bool AlwaysOnTop { get; set; } = true;
    public bool SoundEnabled { get; set; } = true;
}
