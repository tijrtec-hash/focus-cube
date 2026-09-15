using System.IO;
using System.Windows.Media;

namespace FocusCube;

public static class CompletionSoundService
{
    public const string SoftPreset = "soft";
    public const string DigitalPreset = "digital";
    public const string BellPreset = "bell";
    public const string CustomPreset = "custom";

    private static MediaPlayer? _player;

    public static void Play(FocusCubeSettings settings) => PlayCore(settings, requireEnabled: true);

    public static void Preview(FocusCubeSettings settings) => PlayCore(settings, requireEnabled: false);

    private static void PlayCore(FocusCubeSettings settings, bool requireEnabled)
    {
        if (requireEnabled && !settings.SoundEnabled)
            return;

        try
        {
            var path = ResolveSoundPath(settings);
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                return;

            _player?.Stop();
            _player?.Close();

            _player = new MediaPlayer
            {
                Volume = 0.90
            };
            _player.Open(new Uri(path, UriKind.Absolute));
            _player.Play();
        }
        catch
        {
            // A sound failure must never interrupt the timer.
        }
    }

    public static string GetDisplayName(string? preset)
        => preset switch
        {
            DigitalPreset => "Digital",
            BellPreset => "Sino",
            CustomPreset => "Personalizado",
            _ => "Suave"
        };

    private static string? ResolveSoundPath(FocusCubeSettings settings)
    {
        if (string.Equals(settings.CompletionSound, CustomPreset, StringComparison.OrdinalIgnoreCase))
        {
            if (!string.IsNullOrWhiteSpace(settings.CustomSoundPath) && File.Exists(settings.CustomSoundPath))
                return settings.CustomSoundPath;

            // Missing custom file falls back to the default built-in tone.
            return EnsureBuiltInSound(SoftPreset);
        }

        var preset = settings.CompletionSound switch
        {
            DigitalPreset => DigitalPreset,
            BellPreset => BellPreset,
            _ => SoftPreset
        };

        return EnsureBuiltInSound(preset);
    }

    private static string EnsureBuiltInSound(string preset)
    {
        var directory = Path.Combine(SettingsStore.SettingsDirectory, "sounds");
        Directory.CreateDirectory(directory);

        var path = Path.Combine(directory, $"{preset}.wav");
        if (File.Exists(path))
            return path;

        var segments = preset switch
        {
            DigitalPreset => new[]
            {
                new ToneSegment(new[] { 880d }, 90, 45, 0.70),
                new ToneSegment(new[] { 1100d }, 90, 45, 0.72),
                new ToneSegment(new[] { 1320d }, 150, 0, 0.75)
            },
            BellPreset => new[]
            {
                new ToneSegment(new[] { 523.25d, 784.88d, 1046.50d }, 620, 0, 0.56, true)
            },
            _ => new[]
            {
                new ToneSegment(new[] { 659.25d }, 180, 35, 0.54),
                new ToneSegment(new[] { 880d }, 330, 0, 0.58, true)
            }
        };

        WriteWave(path, segments);
        return path;
    }

    private static void WriteWave(string path, IReadOnlyList<ToneSegment> segments)
    {
        const int sampleRate = 44100;
        const short channels = 1;
        const short bitsPerSample = 16;

        var samples = new List<short>();
        foreach (var segment in segments)
        {
            var toneSampleCount = (int)Math.Round(sampleRate * segment.DurationMs / 1000d);
            var attackSamples = Math.Max(1, (int)(toneSampleCount * 0.05));
            var releaseSamples = Math.Max(1, (int)(toneSampleCount * (segment.LongRelease ? 0.78 : 0.20)));

            for (var i = 0; i < toneSampleCount; i++)
            {
                var t = i / (double)sampleRate;
                var envelope = 1d;
                if (i < attackSamples)
                    envelope = i / (double)attackSamples;
                else if (i >= toneSampleCount - releaseSamples)
                    envelope = Math.Max(0d, (toneSampleCount - 1 - i) / (double)releaseSamples);

                if (segment.LongRelease)
                    envelope *= Math.Exp(-2.2 * i / (double)toneSampleCount);

                var mixed = 0d;
                foreach (var frequency in segment.Frequencies)
                {
                    mixed += Math.Sin(2d * Math.PI * frequency * t);
                    if (segment.LongRelease)
                        mixed += 0.20 * Math.Sin(2d * Math.PI * frequency * 2d * t);
                }

                mixed /= segment.Frequencies.Count;
                mixed *= segment.Amplitude * envelope;
                mixed = Math.Clamp(mixed, -1d, 1d);
                samples.Add((short)Math.Round(mixed * short.MaxValue));
            }

            var gapSamples = (int)Math.Round(sampleRate * segment.GapMs / 1000d);
            for (var i = 0; i < gapSamples; i++)
                samples.Add(0);
        }

        var byteRate = sampleRate * channels * bitsPerSample / 8;
        var blockAlign = (short)(channels * bitsPerSample / 8);
        var dataSize = samples.Count * sizeof(short);

        using var stream = File.Create(path);
        using var writer = new BinaryWriter(stream);

        writer.Write("RIFF"u8.ToArray());
        writer.Write(36 + dataSize);
        writer.Write("WAVE"u8.ToArray());
        writer.Write("fmt "u8.ToArray());
        writer.Write(16);
        writer.Write((short)1);
        writer.Write(channels);
        writer.Write(sampleRate);
        writer.Write(byteRate);
        writer.Write(blockAlign);
        writer.Write(bitsPerSample);
        writer.Write("data"u8.ToArray());
        writer.Write(dataSize);
        foreach (var sample in samples)
            writer.Write(sample);
    }

    private sealed record ToneSegment(
        IReadOnlyList<double> Frequencies,
        int DurationMs,
        int GapMs,
        double Amplitude,
        bool LongRelease = false);
}
