namespace FocusCube;

/// <summary>
/// Holds the countdown state independently from the WPF rendering layer.
/// The UI clock periodically calls Refresh so the countdown is based on a deadline,
/// avoiding cumulative drift from decrementing one second per tick.
/// </summary>
public sealed class TimerSession
{
    private DateTimeOffset? _deadline;

    public TimerSession(TimeSpan baseDuration)
    {
        if (baseDuration <= TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(baseDuration));

        BaseDuration = baseDuration;
        SessionDuration = baseDuration;
        Remaining = baseDuration;
    }

    public TimeSpan BaseDuration { get; }

    /// <summary>
    /// Duration used as the denominator for progress during the current session.
    /// Quick-add buttons extend this value together with Remaining.
    /// </summary>
    public TimeSpan SessionDuration { get; private set; }

    public TimeSpan Remaining { get; private set; }

    public bool IsRunning { get; private set; }

    public double Progress
        => SessionDuration <= TimeSpan.Zero
            ? 0d
            : Math.Clamp(Remaining.TotalSeconds / SessionDuration.TotalSeconds, 0d, 1d);

    public void Start(DateTimeOffset now)
    {
        if (IsRunning)
            return;

        if (Remaining <= TimeSpan.Zero)
        {
            SessionDuration = BaseDuration;
            Remaining = BaseDuration;
        }

        IsRunning = true;
        _deadline = now + Remaining;
    }

    public void Pause(DateTimeOffset now)
    {
        Refresh(now);
        IsRunning = false;
        _deadline = null;
    }

    public void Toggle(DateTimeOffset now)
    {
        if (IsRunning)
            Pause(now);
        else
            Start(now);
    }

    /// <summary>
    /// Restores the original 60-minute session. Running/paused state is preserved.
    /// </summary>
    public void Reset(DateTimeOffset now)
    {
        SessionDuration = BaseDuration;
        Remaining = BaseDuration;

        if (IsRunning)
            _deadline = now + Remaining;
        else
            _deadline = null;
    }

    /// <summary>
    /// Adds time to the current session. If the previous countdown had already ended,
    /// the added amount becomes the new current session duration but remains paused.
    /// </summary>
    public void Add(TimeSpan amount, DateTimeOffset now)
    {
        if (amount <= TimeSpan.Zero)
            return;

        Refresh(now);

        if (Remaining <= TimeSpan.Zero)
        {
            SessionDuration = amount;
            Remaining = amount;
        }
        else
        {
            SessionDuration += amount;
            Remaining += amount;
        }

        if (IsRunning)
            _deadline = now + Remaining;
    }

    public void Refresh(DateTimeOffset now)
    {
        if (!IsRunning || _deadline is null)
            return;

        var remaining = _deadline.Value - now;
        if (remaining <= TimeSpan.Zero)
        {
            Remaining = TimeSpan.Zero;
            IsRunning = false;
            _deadline = null;
            return;
        }

        Remaining = remaining;
    }
}
