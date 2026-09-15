namespace FocusCube;

public static class TimeInputParser
{
    private static readonly TimeSpan MaximumDuration = TimeSpan.FromDays(7);

    public static bool TryParse(string? text, out TimeSpan duration)
    {
        duration = TimeSpan.Zero;
        if (string.IsNullOrWhiteSpace(text))
            return false;

        var value = text.Trim();

        // Plain number means minutes.
        if (!value.Contains(':'))
        {
            if (!double.TryParse(value, out var minutes) || minutes <= 0)
                return false;

            duration = TimeSpan.FromMinutes(minutes);
            return duration <= MaximumDuration;
        }

        var parts = value.Split(':');
        if (parts.Length is < 2 or > 3)
            return false;

        if (!parts.All(p => int.TryParse(p, out _)))
            return false;

        try
        {
            if (parts.Length == 2)
            {
                var minutes = int.Parse(parts[0]);
                var seconds = int.Parse(parts[1]);
                if (minutes < 0 || seconds is < 0 or > 59)
                    return false;

                duration = TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds);
            }
            else
            {
                var hours = int.Parse(parts[0]);
                var minutes = int.Parse(parts[1]);
                var seconds = int.Parse(parts[2]);
                if (hours < 0 || minutes is < 0 or > 59 || seconds is < 0 or > 59)
                    return false;

                duration = TimeSpan.FromHours(hours) + TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds);
            }
        }
        catch (OverflowException)
        {
            return false;
        }

        return duration > TimeSpan.Zero && duration <= MaximumDuration;
    }

    public static string Format(TimeSpan duration)
    {
        var totalSeconds = Math.Max(0L, (long)Math.Ceiling(duration.TotalSeconds));
        if (totalSeconds >= 3600)
        {
            var hours = totalSeconds / 3600;
            var minutes = (totalSeconds % 3600) / 60;
            var seconds = totalSeconds % 60;
            return $"{hours}:{minutes:00}:{seconds:00}";
        }

        var totalMinutes = totalSeconds / 60;
        return $"{totalMinutes:00}:{totalSeconds % 60:00}";
    }
}
