using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Microsoft.Win32;

namespace FocusCube;

public partial class MainWindow : Window
{
    private static readonly TimeSpan DefaultDuration = TimeSpan.FromMinutes(60);
    private const double ExpandedHeight = 251d;
    private const double CompactHeight = 170d;
    private const double DefaultWidth = 170d;
    private const double DrawerRetractedY = -650d;

    private readonly TimerSession _session = new(DefaultDuration);
    private readonly DispatcherTimer _uiTimer;
    private readonly FocusCubeSettings _settings;
    private readonly ContextMenu _moreMenu;

    private bool _drawerExpanded;
    private bool _completionNotified;
    private bool _isLoaded;
    private MenuItem? _soundMenuItem;
    private MenuItem? _topmostMenuItem;
    private MenuItem? _drawerMenuItem;
    private MenuItem? _softSoundMenuItem;
    private MenuItem? _digitalSoundMenuItem;
    private MenuItem? _bellSoundMenuItem;
    private MenuItem? _customSoundMenuItem;

    public MainWindow()
    {
        InitializeComponent();

        _settings = SettingsStore.Load();
        _drawerExpanded = _settings.DrawerExpanded;
        Topmost = _settings.AlwaysOnTop;
        Width = DefaultWidth;
        Height = _drawerExpanded ? ExpandedHeight : CompactHeight;
        DrawerTranslate.Y = _drawerExpanded ? 0d : DrawerRetractedY;
        UpdateChevron();

        _moreMenu = BuildMoreMenu();
        MoreButton.ContextMenu = _moreMenu;

        _uiTimer = new DispatcherTimer(DispatcherPriority.Render)
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        _uiTimer.Tick += UiTimer_Tick;

        Loaded += MainWindow_Loaded;
        Closed += MainWindow_Closed;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        RestorePosition();

        var now = DateTimeOffset.UtcNow;
        _session.Start(now);
        RefreshUi(now);
        _uiTimer.Start();
        _isLoaded = true;
    }

    private void MainWindow_Closed(object? sender, EventArgs e)
    {
        _uiTimer.Stop();
        PersistSettings();
    }

    private void UiTimer_Tick(object? sender, EventArgs e)
    {
        RefreshUi(DateTimeOffset.UtcNow);
    }

    private void RefreshUi(DateTimeOffset now)
    {
        _session.Refresh(now);

        TimeText.Text = TimeInputParser.Format(_session.Remaining);
        TimeText.FontSize = TimeText.Text.Length switch
        {
            <= 5 => 186,
            <= 7 => 152,
            _ => 122
        };

        if (_session.IsCompleted)
        {
            TimerProgressRing.Progress = 1;
            TimerProgressRing.UseProgressColor = false;
            TimerProgressRing.RingBrush = new SolidColorBrush(Color.FromRgb(0xFF, 0x3B, 0x30));
            ModeText.Text = "FIM";
            ModeText.Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0x8A, 0x83));

            if (!_completionNotified)
            {
                _completionNotified = true;
                NotifyCompletion();
            }
        }
        else
        {
            _completionNotified = false;
            TimerProgressRing.Progress = _session.Progress;
            TimerProgressRing.UseProgressColor = true;
            ModeText.Text = "TIMER";
            ModeText.Foreground = new SolidColorBrush(Color.FromRgb(0xAE, 0xB3, 0xB8));
            CompletionGlow.BeginAnimation(OpacityProperty, null);
            CompletionGlow.Opacity = 0;
        }

        UpdatePausePlayVisual();
    }

    private void UpdatePausePlayVisual()
    {
        var running = _session.IsRunning;
        PauseGlyph.Visibility = running ? Visibility.Visible : Visibility.Collapsed;
        PlayGlyph.Visibility = running ? Visibility.Collapsed : Visibility.Visible;
        PauseButton.ToolTip = running ? "Pausar" : "Continuar";
    }

    private void NotifyCompletion()
    {
        CompletionSoundService.Play(_settings);

        var pulse = new DoubleAnimation
        {
            From = 0.08,
            To = 0.95,
            Duration = TimeSpan.FromMilliseconds(260),
            AutoReverse = true,
            RepeatBehavior = new RepeatBehavior(4)
        };
        CompletionGlow.BeginAnimation(OpacityProperty, pulse);
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

    private void DrawerToggle_Click(object sender, RoutedEventArgs e)
    {
        SetDrawerExpanded(!_drawerExpanded, animate: true);
    }

    private void SetDrawerExpanded(bool expanded, bool animate)
    {
        if (_drawerExpanded == expanded && _isLoaded)
            return;

        var oldHeight = Height;
        var targetHeight = expanded ? ExpandedHeight : CompactHeight;
        var oldDrawerY = DrawerTranslate.Y;
        var targetDrawerY = expanded ? 0d : DrawerRetractedY;
        var wasNearBottom = _isLoaded && DesktopDocking.IsNearBottom(this);
        var oldTop = Top;
        var targetTop = wasNearBottom ? oldTop + oldHeight - targetHeight : oldTop;

        _drawerExpanded = expanded;
        _settings.DrawerExpanded = expanded;
        UpdateChevron();
        if (_drawerMenuItem is not null)
            _drawerMenuItem.Header = expanded ? "Recolher controles" : "Expandir controles";

        if (!animate || !_isLoaded)
        {
            DrawerTranslate.Y = targetDrawerY;
            Height = targetHeight;
            if (wasNearBottom)
                Top = targetTop;
            PersistSettings();
            return;
        }

        // Physical drawer motion: the lower module slides upward behind the body.
        // Quintic EaseOut gives a fast initial movement and a soft landing at the end.
        var easing = new QuinticEase { EasingMode = EasingMode.EaseOut };
        var duration = TimeSpan.FromMilliseconds(300);

        var drawerAnimation = new DoubleAnimation(oldDrawerY, targetDrawerY, duration)
        {
            EasingFunction = easing
        };
        drawerAnimation.Completed += (_, _) =>
        {
            DrawerTranslate.BeginAnimation(TranslateTransform.YProperty, null);
            DrawerTranslate.Y = targetDrawerY;
        };
        DrawerTranslate.BeginAnimation(TranslateTransform.YProperty, drawerAnimation, HandoffBehavior.SnapshotAndReplace);

        var heightAnimation = new DoubleAnimation(oldHeight, targetHeight, duration)
        {
            EasingFunction = easing
        };
        heightAnimation.Completed += (_, _) =>
        {
            BeginAnimation(HeightProperty, null);
            Height = targetHeight;
            if (wasNearBottom)
            {
                BeginAnimation(TopProperty, null);
                Top = targetTop;
            }
            PersistSettings();
        };
        BeginAnimation(HeightProperty, heightAnimation, HandoffBehavior.SnapshotAndReplace);

        if (wasNearBottom)
        {
            var topAnimation = new DoubleAnimation(oldTop, targetTop, duration)
            {
                EasingFunction = easing
            };
            BeginAnimation(TopProperty, topAnimation, HandoffBehavior.SnapshotAndReplace);
        }
    }

    private void UpdateChevron()
    {
        DrawerChevron.Data = Geometry.Parse(
            _drawerExpanded
                ? "M 651,1272 L 694,1228 L 737,1272"
                : "M 651,1228 L 694,1272 L 737,1228");
    }

    private ContextMenu BuildMoreMenu()
    {
        var menu = new ContextMenu
        {
            Placement = PlacementMode.Bottom,
            StaysOpen = false
        };

        menu.Items.Add(MenuItem("Definir tempo…", (_, _) => OpenCustomTimeDialog()));
        menu.Items.Add(new Separator());
        menu.Items.Add(MenuItem("5 min", (_, _) => SetExactDuration(TimeSpan.FromMinutes(5))));
        menu.Items.Add(MenuItem("25 min", (_, _) => SetExactDuration(TimeSpan.FromMinutes(25))));
        menu.Items.Add(MenuItem("60 min", (_, _) => SetExactDuration(TimeSpan.FromMinutes(60))));
        menu.Items.Add(new Separator());

        _soundMenuItem = MenuItem("Som ao terminar", (_, _) =>
        {
            _settings.SoundEnabled = _soundMenuItem?.IsChecked == true;
            PersistSettings();
        });
        _soundMenuItem.IsCheckable = true;
        _soundMenuItem.IsChecked = _settings.SoundEnabled;
        menu.Items.Add(_soundMenuItem);

        var toneMenu = new MenuItem { Header = "Toque de fim" };
        _softSoundMenuItem = CheckableMenuItem("Suave", (_, _) => SelectCompletionSound(CompletionSoundService.SoftPreset));
        _digitalSoundMenuItem = CheckableMenuItem("Digital", (_, _) => SelectCompletionSound(CompletionSoundService.DigitalPreset));
        _bellSoundMenuItem = CheckableMenuItem("Sino", (_, _) => SelectCompletionSound(CompletionSoundService.BellPreset));
        _customSoundMenuItem = CheckableMenuItem("Personalizado…", (_, _) => ChooseCustomSound());

        toneMenu.Items.Add(_softSoundMenuItem);
        toneMenu.Items.Add(_digitalSoundMenuItem);
        toneMenu.Items.Add(_bellSoundMenuItem);
        toneMenu.Items.Add(new Separator());
        toneMenu.Items.Add(_customSoundMenuItem);
        toneMenu.Items.Add(MenuItem("Testar toque", (_, _) => CompletionSoundService.Preview(_settings)));
        menu.Items.Add(toneMenu);

        _topmostMenuItem = MenuItem("Sempre no topo", (_, _) =>
        {
            _settings.AlwaysOnTop = _topmostMenuItem?.IsChecked == true;
            Topmost = _settings.AlwaysOnTop;
            PersistSettings();
        });
        _topmostMenuItem.IsCheckable = true;
        _topmostMenuItem.IsChecked = _settings.AlwaysOnTop;
        menu.Items.Add(_topmostMenuItem);

        _drawerMenuItem = MenuItem(_drawerExpanded ? "Recolher controles" : "Expandir controles", (_, _) =>
        {
            SetDrawerExpanded(!_drawerExpanded, animate: true);
        });
        menu.Items.Add(_drawerMenuItem);

        menu.Items.Add(new Separator());
        menu.Items.Add(MenuItem("Sair", (_, _) => Close()));

        UpdateSoundMenuChecks();
        return menu;
    }

    private void SelectCompletionSound(string preset)
    {
        _settings.CompletionSound = preset;
        _settings.SoundEnabled = true;
        if (_soundMenuItem is not null)
            _soundMenuItem.IsChecked = true;

        UpdateSoundMenuChecks();
        PersistSettings();
        CompletionSoundService.Preview(_settings);
    }

    private void ChooseCustomSound()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Escolher som de fim do timer",
            Filter = "Áudio compatível (*.wav;*.mp3;*.wma)|*.wav;*.mp3;*.wma|Todos os arquivos (*.*)|*.*",
            CheckFileExists = true,
            Multiselect = false
        };

        if (!string.IsNullOrWhiteSpace(_settings.CustomSoundPath))
        {
            try
            {
                var directory = Path.GetDirectoryName(_settings.CustomSoundPath);
                if (!string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory))
                    dialog.InitialDirectory = directory;
            }
            catch
            {
                // Ignore an invalid previous custom path.
            }
        }

        if (dialog.ShowDialog(this) != true)
        {
            UpdateSoundMenuChecks();
            return;
        }

        _settings.CustomSoundPath = dialog.FileName;
        _settings.CompletionSound = CompletionSoundService.CustomPreset;
        _settings.SoundEnabled = true;
        if (_soundMenuItem is not null)
            _soundMenuItem.IsChecked = true;

        UpdateSoundMenuChecks();
        PersistSettings();
        CompletionSoundService.Preview(_settings);
    }

    private void UpdateSoundMenuChecks()
    {
        var preset = _settings.CompletionSound;
        if (_softSoundMenuItem is not null)
            _softSoundMenuItem.IsChecked = string.Equals(preset, CompletionSoundService.SoftPreset, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(preset);
        if (_digitalSoundMenuItem is not null)
            _digitalSoundMenuItem.IsChecked = string.Equals(preset, CompletionSoundService.DigitalPreset, StringComparison.OrdinalIgnoreCase);
        if (_bellSoundMenuItem is not null)
            _bellSoundMenuItem.IsChecked = string.Equals(preset, CompletionSoundService.BellPreset, StringComparison.OrdinalIgnoreCase);
        if (_customSoundMenuItem is not null)
        {
            _customSoundMenuItem.IsChecked = string.Equals(preset, CompletionSoundService.CustomPreset, StringComparison.OrdinalIgnoreCase);
            _customSoundMenuItem.Header = string.IsNullOrWhiteSpace(_settings.CustomSoundPath)
                ? "Personalizado…"
                : $"Personalizado… ({Path.GetFileName(_settings.CustomSoundPath)})";
        }
    }

    private static MenuItem MenuItem(string header, RoutedEventHandler handler)
    {
        var item = new MenuItem { Header = header };
        item.Click += handler;
        return item;
    }

    private static MenuItem CheckableMenuItem(string header, RoutedEventHandler handler)
    {
        var item = MenuItem(header, handler);
        item.IsCheckable = true;
        item.StaysOpenOnClick = false;
        return item;
    }

    private void More_Click(object sender, RoutedEventArgs e)
    {
        if (_soundMenuItem is not null)
            _soundMenuItem.IsChecked = _settings.SoundEnabled;
        if (_topmostMenuItem is not null)
            _topmostMenuItem.IsChecked = Topmost;
        if (_drawerMenuItem is not null)
            _drawerMenuItem.Header = _drawerExpanded ? "Recolher controles" : "Expandir controles";

        UpdateSoundMenuChecks();
        _moreMenu.PlacementTarget = MoreButton;
        _moreMenu.IsOpen = true;
    }

    private void OpenCustomTimeDialog()
    {
        var dialog = new CustomTimeDialog(_session.ResetDuration)
        {
            Owner = this,
            Topmost = this.Topmost
        };

        if (dialog.ShowDialog() == true)
            SetExactDuration(dialog.SelectedDuration);
    }

    private void SetExactDuration(TimeSpan duration)
    {
        var now = DateTimeOffset.UtcNow;
        _session.SetDuration(duration, now, startImmediately: true);
        RefreshUi(now);
    }

    private void RestorePosition()
    {
        if (_settings.Left is double left && _settings.Top is double top)
        {
            var clamped = DesktopDocking.ClampToWorkArea(this, left, top);
            Left = clamped.X;
            Top = clamped.Y;
            return;
        }

        var work = DesktopDocking.GetWorkArea(this);
        Left = work.Right - Width - 18;
        Top = work.Top + 18;
    }

    private void PersistSettings()
    {
        if (!_isLoaded)
            return;

        _settings.Left = Left;
        _settings.Top = Top;
        _settings.DrawerExpanded = _drawerExpanded;
        _settings.AlwaysOnTop = Topmost;
        SettingsStore.Save(_settings);
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        if (e.ButtonState != MouseButtonState.Pressed || IsInsideButton(e.OriginalSource as DependencyObject))
            return;

        try
        {
            DragMove();
            DesktopDocking.Snap(this);
            PersistSettings();
        }
        catch (InvalidOperationException)
        {
            // DragMove can throw if the mouse button state changes between the event and this call.
        }
    }

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
            Pause_Click(this, new RoutedEventArgs());
        }
        else if (e.Key == Key.R)
        {
            e.Handled = true;
            Reset_Click(this, new RoutedEventArgs());
        }
        else if (e.Key == Key.E)
        {
            e.Handled = true;
            SetDrawerExpanded(!_drawerExpanded, animate: true);
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
