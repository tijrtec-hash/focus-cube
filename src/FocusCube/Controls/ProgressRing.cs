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
        new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(39, 48, 48)), FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
        nameof(Thickness),
        typeof(double),
        typeof(ProgressRing),
        new FrameworkPropertyMetadata(5d, FrameworkPropertyMetadataOptions.AffectsRender));

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

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        var stroke = Math.Max(1, Thickness);
        var radius = Math.Max(0, Math.Min(ActualWidth, ActualHeight) / 2 - stroke / 2);
        var center = new Point(ActualWidth / 2, ActualHeight / 2);

        var trackPen = new Pen(TrackBrush, stroke)
        {
            StartLineCap = PenLineCap.Round,
            EndLineCap = PenLineCap.Round
        };
        drawingContext.DrawEllipse(null, trackPen, center, radius, radius);

        if (Progress <= 0 || radius <= 0)
            return;

        var progressPen = new Pen(RingBrush, stroke)
        {
            StartLineCap = PenLineCap.Round,
            EndLineCap = PenLineCap.Round
        };

        if (Progress >= 0.9999)
        {
            drawingContext.DrawEllipse(null, progressPen, center, radius, radius);
            return;
        }

        var startAngle = -90d;
        var sweepAngle = 360d * Progress;
        var endAngle = startAngle + sweepAngle;

        var start = PointOnCircle(center, radius, startAngle);
        var end = PointOnCircle(center, radius, endAngle);

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
        drawingContext.DrawGeometry(null, progressPen, geometry);
    }

    private static object CoerceProgress(DependencyObject d, object baseValue)
        => Math.Clamp((double)baseValue, 0d, 1d);

    private static Point PointOnCircle(Point center, double radius, double degrees)
    {
        var radians = degrees * Math.PI / 180d;
        return new Point(
            center.X + radius * Math.Cos(radians),
            center.Y + radius * Math.Sin(radians));
    }
}
