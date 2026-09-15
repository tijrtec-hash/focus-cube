using System.Windows;
using System.Windows.Media;

namespace FocusCube.Controls;

public sealed class ProgressRing : FrameworkElement
{
    public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(
        nameof(Progress),
        typeof(double),
        typeof(ProgressRing),
        new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender, null, CoerceProgress));

    public static readonly DependencyProperty RingBrushProperty = DependencyProperty.Register(
        nameof(RingBrush),
        typeof(Brush),
        typeof(ProgressRing),
        new FrameworkPropertyMetadata(Brushes.LimeGreen, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty TrackBrushProperty = DependencyProperty.Register(
        nameof(TrackBrush),
        typeof(Brush),
        typeof(ProgressRing),
        new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(55, 59, 61)), FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
        nameof(Thickness),
        typeof(double),
        typeof(ProgressRing),
        new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty SegmentCountProperty = DependencyProperty.Register(
        nameof(SegmentCount),
        typeof(int),
        typeof(ProgressRing),
        new FrameworkPropertyMetadata(12, FrameworkPropertyMetadataOptions.AffectsRender, null, CoerceSegmentCount));

    public static readonly DependencyProperty GapDegreesProperty = DependencyProperty.Register(
        nameof(GapDegrees),
        typeof(double),
        typeof(ProgressRing),
        new FrameworkPropertyMetadata(7d, FrameworkPropertyMetadataOptions.AffectsRender, null, CoerceGapDegrees));

    public static readonly DependencyProperty UseProgressColorProperty = DependencyProperty.Register(
        nameof(UseProgressColor),
        typeof(bool),
        typeof(ProgressRing),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    public Brush RingBrush
    {
        get => (Brush)GetValue(RingBrushProperty);
        set => SetValue(RingBrushProperty, value);
    }

    public Brush TrackBrush
    {
        get => (Brush)GetValue(TrackBrushProperty);
        set => SetValue(TrackBrushProperty, value);
    }

    public double Thickness
    {
        get => (double)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    public int SegmentCount
    {
        get => (int)GetValue(SegmentCountProperty);
        set => SetValue(SegmentCountProperty, value);
    }

    public double GapDegrees
    {
        get => (double)GetValue(GapDegreesProperty);
        set => SetValue(GapDegreesProperty, value);
    }

    public bool UseProgressColor
    {
        get => (bool)GetValue(UseProgressColorProperty);
        set => SetValue(UseProgressColorProperty, value);
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        var stroke = Math.Max(1, Thickness);
        var radius = Math.Max(0, Math.Min(ActualWidth, ActualHeight) / 2 - stroke / 2);
        if (radius <= 0)
            return;

        var center = new Point(ActualWidth / 2, ActualHeight / 2);
        var count = Math.Max(1, SegmentCount);
        var slotDegrees = 360d / count;
        var gap = Math.Min(Math.Max(0, GapDegrees), slotDegrees - 0.5);
        var segmentDegrees = slotDegrees - gap;

        var trackPen = CreatePen(TrackBrush, stroke);
        for (var i = 0; i < count; i++)
        {
            var startAngle = -90d + i * slotDegrees + gap / 2d;
            DrawArc(drawingContext, center, radius, startAngle, segmentDegrees, trackPen);
        }

        if (Progress <= 0)
            return;

        var activeBrush = UseProgressColor ? new SolidColorBrush(GetProgressColor(Progress)) : RingBrush;
        if (activeBrush.CanFreeze)
            activeBrush.Freeze();

        var progressPen = CreatePen(activeBrush, stroke);
        var remainingDegrees = 360d * Progress;

        for (var i = 0; i < count && remainingDegrees > 0; i++)
        {
            var visibleDegrees = Math.Min(segmentDegrees, remainingDegrees);
            if (visibleDegrees > 0.25)
            {
                var startAngle = -90d + i * slotDegrees + gap / 2d;
                DrawArc(drawingContext, center, radius, startAngle, visibleDegrees, progressPen);
            }

            remainingDegrees -= slotDegrees;
        }
    }

    private static Pen CreatePen(Brush brush, double thickness)
    {
        var pen = new Pen(brush, thickness)
        {
            StartLineCap = PenLineCap.Round,
            EndLineCap = PenLineCap.Round
        };

        if (pen.CanFreeze)
            pen.Freeze();

        return pen;
    }

    private static void DrawArc(
        DrawingContext drawingContext,
        Point center,
        double radius,
        double startAngle,
        double sweepAngle,
        Pen pen)
    {
        if (sweepAngle <= 0.01)
            return;

        var start = PointOnCircle(center, radius, startAngle);
        var end = PointOnCircle(center, radius, startAngle + sweepAngle);

        var geometry = new StreamGeometry();
        using (var context = geometry.Open())
        {
            context.BeginFigure(start, false, false);
            context.ArcTo(
                end,
                new Size(radius, radius),
                0,
                sweepAngle > 180,
                SweepDirection.Clockwise,
                true,
                false);
        }

        geometry.Freeze();
        drawingContext.DrawGeometry(null, pen, geometry);
    }

    private static Color GetProgressColor(double progress)
    {
        progress = Math.Clamp(progress, 0d, 1d);

        var green = Color.FromRgb(0x55, 0xDB, 0x8C);
        var yellow = Color.FromRgb(0xFF, 0xD8, 0x4D);
        var orange = Color.FromRgb(0xFF, 0x98, 0x2E);
        var red = Color.FromRgb(0xFF, 0x3B, 0x30);

        if (progress >= 0.55)
            return green;

        if (progress >= 0.30)
            return Lerp(yellow, green, (progress - 0.30) / 0.25);

        if (progress >= 0.15)
            return Lerp(orange, yellow, (progress - 0.15) / 0.15);

        return Lerp(red, orange, progress / 0.15);
    }

    private static Color Lerp(Color from, Color to, double amount)
    {
        amount = Math.Clamp(amount, 0d, 1d);
        return Color.FromRgb(
            (byte)Math.Round(from.R + (to.R - from.R) * amount),
            (byte)Math.Round(from.G + (to.G - from.G) * amount),
            (byte)Math.Round(from.B + (to.B - from.B) * amount));
    }

    private static object CoerceProgress(DependencyObject d, object baseValue)
        => Math.Clamp((double)baseValue, 0d, 1d);

    private static object CoerceSegmentCount(DependencyObject d, object baseValue)
        => Math.Clamp((int)baseValue, 1, 64);

    private static object CoerceGapDegrees(DependencyObject d, object baseValue)
        => Math.Clamp((double)baseValue, 0d, 30d);

    private static Point PointOnCircle(Point center, double radius, double degrees)
    {
        var radians = degrees * Math.PI / 180d;
        return new Point(
            center.X + radius * Math.Cos(radians),
            center.Y + radius * Math.Sin(radians));
    }
}
