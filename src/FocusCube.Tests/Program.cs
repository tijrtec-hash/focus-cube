using FocusCube;

var failures = new List<string>();

void Check(string name, bool condition)
{
    if (!condition)
        failures.Add(name);
}

var t0 = new DateTimeOffset(2026, 9, 15, 0, 0, 0, TimeSpan.Zero);

var session = new TimerSession(TimeSpan.FromMinutes(60));
Check("initial remaining", session.Remaining == TimeSpan.FromMinutes(60));
Check("initial reset duration", session.ResetDuration == TimeSpan.FromMinutes(60));

session.Start(t0);
session.Refresh(t0.AddMinutes(10));
Check("countdown by deadline", Math.Abs((session.Remaining - TimeSpan.FromMinutes(50)).TotalMilliseconds) < 1);
Check("progress after ten minutes", Math.Abs(session.Progress - (50d / 60d)) < 0.0001);

session.Pause(t0.AddMinutes(10));
session.Refresh(t0.AddMinutes(20));
Check("pause freezes remaining", Math.Abs((session.Remaining - TimeSpan.FromMinutes(50)).TotalMilliseconds) < 1);

session.Add(TimeSpan.FromMinutes(10), t0.AddMinutes(20));
Check("quick add remaining", Math.Abs((session.Remaining - TimeSpan.FromMinutes(60)).TotalMilliseconds) < 1);
Check("quick add denominator", session.SessionDuration == TimeSpan.FromMinutes(70));
Check("quick add does not change reset target", session.ResetDuration == TimeSpan.FromMinutes(60));

session.Reset(t0.AddMinutes(20));
Check("reset target", session.Remaining == TimeSpan.FromMinutes(60));
Check("reset preserves paused", !session.IsRunning);

session.SetDuration(TimeSpan.FromMinutes(25), t0.AddMinutes(20));
Check("custom duration remaining", session.Remaining == TimeSpan.FromMinutes(25));
Check("custom reset duration", session.ResetDuration == TimeSpan.FromMinutes(25));
Check("custom starts", session.IsRunning);

session.Refresh(t0.AddMinutes(45));
Check("completion", session.IsCompleted && !session.IsRunning && session.Remaining == TimeSpan.Zero);

session.Add(TimeSpan.FromMinutes(5), t0.AddMinutes(46));
Check("add after completion", session.Remaining == TimeSpan.FromMinutes(5));
Check("add after completion paused", !session.IsRunning);

Check("parse minutes", TimeInputParser.TryParse("25", out var p1) && p1 == TimeSpan.FromMinutes(25));
Check("parse mmss", TimeInputParser.TryParse("05:30", out var p2) && p2 == TimeSpan.FromMinutes(5.5));
Check("parse hmmss", TimeInputParser.TryParse("1:02:03", out var p3) && p3 == new TimeSpan(1, 2, 3));
Check("reject zero", !TimeInputParser.TryParse("0", out _));
Check("reject bad seconds", !TimeInputParser.TryParse("05:99", out _));
Check("format under hour", TimeInputParser.Format(TimeSpan.FromSeconds(65)) == "01:05");
Check("format hour", TimeInputParser.Format(new TimeSpan(1, 2, 3)) == "1:02:03");

if (failures.Count > 0)
{
    Console.Error.WriteLine("FocusCube logic tests failed:");
    foreach (var failure in failures)
        Console.Error.WriteLine($"- {failure}");
    Environment.Exit(1);
}

Console.WriteLine("FocusCube logic tests passed.");
