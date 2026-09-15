using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FocusCube;

public partial class MainWindow : Window
{
    private static readonly TimeSpan DefaultDuration = TimeSpan.FromMinutes(60);

    private readonly TimerSession _session = new(DefaultDuration);
    private readonly DispatcherTimer _uiTimer;

    public MainWindow()
    {
        InitializeComponent();

        _uiTimer = new DispatcherTimer(DispatcherPriority.Render)
        {
            Interval = TimeSpan.FromMilliseconds(200)
        };
        _uiTimer.Tick += UiTimer_Tick;

        Loaded += MainWindow_Loaded;
        Closed += MainWindow_Closed;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var now = DateTimeOffset.UtcNow;
        _session.Start(now);
        RefreshUi(now);
        _uiTimer.Start();
    }

    private void MainWindow_Closed(object? sender, EventArgs e)
    {
        _uiTimer.Stop();
    }

    private void UiTimer_Tick(object? sender, EventArgs e)
    {
        RefreshUi(DateTimeOffset.UtcNow);
    }

    private void RefreshUi(DateTimeOffset now)
    {
        _session.Refresh(now);

        var totalSeconds = Math.Max(0L, (long)Math.Ceiling(_session.Remaining.TotalSeconds));
        var minutes = totalSeconds / 60;
        var seconds = totalSeconds % 60;

        TimeText.Text = $"{minutes:00}:{seconds:00}";
        TimerProgressRing.Progress = _session.Progress;
        PauseButton.ToolTip = _session.IsRunning ? "Pausar" : "Continuar";
    }

    private void Add5_Click(object sender, RoutedEventArgs e) => AddMinutes(5);

    private void Add10_Click(object sender, RoutedEventArgs e) => AddMinutes(10);

    private void Add30_Click(object sender, RoutedEventArgs e) => AddMinutes(30);

    private void Add60_Click(object sender, RoutedEventArgs e) => AddMinutes(60);

    private void AddMinutes(int minutes)
    {
        var now = DateTimeOffset.UtcNow;
        _session.Add(TimeSpan.FromMinutes(minutes), now);
        RefreshUi(now);
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        var now = DateTimeOffset.UtcNow;
        _session.Reset(now);
        RefreshUi(now);
    }

    private void Pause_Click(object sender, RoutedEventArgs e)
    {
        var now = DateTimeOffset.UtcNow;
        _session.Toggle(now);
        RefreshUi(now);
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        if (e.ButtonState != MouseButtonState.Pressed || IsInsideButton(e.OriginalSource as DependencyObject))
            return;

        try
        {
            DragMove();
        }
        catch (InvalidOperationException)
        {
            // DragMove can throw if the mouse button state changes between the event and this call.
        }
    }

    private static bool IsInsideButton(DependencyObject? element)
    {
        while (element is not null)
        {
            if (element is ButtonBase)
                return true;

            element = VisualTreeHelper.GetParent(element);
        }

        return false;
    }
}
