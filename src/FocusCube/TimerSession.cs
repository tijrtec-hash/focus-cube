namespace FocusCube;

/// <summary>
/// Holds countdown state independently from the WPF rendering layer.
/// The UI periodically calls Refresh so the countdown is based on a deadline,
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
        ResetDuration = baseDuration;
        SessionDuration = baseDuration;
        Remaining = baseDuration;
    }

    public TimeSpan BaseDuration { get; }

    /// <summary>
    /// Duration restored by Reset. It starts at BaseDuration and changes when
    /// the user explicitly selects a new exact/custom duration.
    /// </summary>
    public TimeSpan ResetDuration { get; private set; }

    /// <summary>
    /// Duration used as the denominator for progress during the current session.
    /// Quick-add buttons extend this value together with Remaining.
    /// </summary>
    public TimeSpan SessionDuration { get; private set; }

    public TimeSpan Remaining { get; private set; }

    public bool IsRunning { get; private set; }

    public bool IsCompleted => Remaining <= TimeSpan.Zero;

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
            SessionDuration = ResetDuration;
            Remaining = ResetDuration;
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
    /// Restores the currently selected exact duration. Running/paused state is preserved.
    /// </summary>
    public void Reset(DateTimeOffset now)
    {
        SessionDuration = ResetDuration;
        Remaining = ResetDuration;

        if (IsRunning)
            _deadline = now + Remaining;
        else
            _deadline = null;
    }

    /// <summary>
    /// Replaces the current session by an exact duration and makes it the new reset target.
    /// </summary>
    public void SetDuration(TimeSpan duration, DateTimeOffset now, bool startImmediately = true)
    {
        if (duration <= TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(duration));

        ResetDuration = duration;
        SessionDuration = duration;
        Remaining = duration;
        IsRunning = startImmediately;
        _deadline = startImmediately ? now + duration : null;
    }

    /// <summary>
    /// Adds time to the current session. If the previous countdown had already ended,
    /// the added amount becomes the new current session duration but remains paused.
    /// Quick-add does not change ResetDuration.
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
