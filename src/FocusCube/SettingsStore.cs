using System.IO;
using System.Text.Json;

namespace FocusCube;

public static class SettingsStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public static string SettingsDirectory
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FocusCube");

    public static string SettingsPath => Path.Combine(SettingsDirectory, "settings.json");

    public static FocusCubeSettings Load()
    {
        try
        {
            if (!File.Exists(SettingsPath))
                return new FocusCubeSettings();

            var json = File.ReadAllText(SettingsPath);
            return JsonSerializer.Deserialize<FocusCubeSettings>(json, JsonOptions) ?? new FocusCubeSettings();
        }
        catch
        {
            // A damaged preference file must never prevent the timer from opening.
            return new FocusCubeSettings();
        }
    }

    public static void Save(FocusCubeSettings settings)
    {
        try
        {
            Directory.CreateDirectory(SettingsDirectory);
            File.WriteAllText(SettingsPath, JsonSerializer.Serialize(settings, JsonOptions));
        }
        catch
        {
            // Preferences are best-effort; timer operation must continue if persistence fails.
        }
    }
}
