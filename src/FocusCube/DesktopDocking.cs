using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace FocusCube;

public static class DesktopDocking
{
    private const uint MonitorDefaultToNearest = 2;

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct MonitorInfo
    {
        public int Size;
        public NativeRect Monitor;
        public NativeRect Work;
        public uint Flags;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint flags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetMonitorInfo(IntPtr monitor, ref MonitorInfo info);

    public static Rect GetWorkArea(Window window)
    {
        var handle = new WindowInteropHelper(window).Handle;
        if (handle == IntPtr.Zero)
            return SystemParameters.WorkArea;

        var monitor = MonitorFromWindow(handle, MonitorDefaultToNearest);
        var info = new MonitorInfo { Size = Marshal.SizeOf<MonitorInfo>() };
        if (monitor == IntPtr.Zero || !GetMonitorInfo(monitor, ref info))
            return SystemParameters.WorkArea;

        var dpi = VisualTreeHelper.GetDpi(window);
        var scaleX = dpi.DpiScaleX <= 0 ? 1d : dpi.DpiScaleX;
        var scaleY = dpi.DpiScaleY <= 0 ? 1d : dpi.DpiScaleY;

        return new Rect(
            info.Work.Left / scaleX,
            info.Work.Top / scaleY,
            (info.Work.Right - info.Work.Left) / scaleX,
            (info.Work.Bottom - info.Work.Top) / scaleY);
    }

    public static void Snap(Window window, double threshold = 28d, double margin = 10d)
    {
        var work = GetWorkArea(window);
        var left = window.Left;
        var top = window.Top;

        var targetLeft = work.Left + margin;
        var targetRight = work.Right - margin - window.Width;
        var targetTop = work.Top + margin;
        var targetBottom = work.Bottom - margin - window.Height;

        if (Math.Abs(left - targetLeft) <= threshold)
            left = targetLeft;
        else if (Math.Abs(left - targetRight) <= threshold)
            left = targetRight;

        if (Math.Abs(top - targetTop) <= threshold)
            top = targetTop;
        else if (Math.Abs(top - targetBottom) <= threshold)
            top = targetBottom;

        window.Left = left;
        window.Top = top;
    }

    public static Point ClampToWorkArea(Window window, double left, double top, double margin = 10d)
    {
        var work = GetWorkArea(window);
        var maxLeft = Math.Max(work.Left + margin, work.Right - window.Width - margin);
        var maxTop = Math.Max(work.Top + margin, work.Bottom - window.Height - margin);

        return new Point(
            Math.Clamp(left, work.Left + margin, maxLeft),
            Math.Clamp(top, work.Top + margin, maxTop));
    }

    public static bool IsNearBottom(Window window, double tolerance = 18d, double margin = 10d)
    {
        var work = GetWorkArea(window);
        var targetBottom = work.Bottom - margin;
        return Math.Abs((window.Top + window.Height) - targetBottom) <= tolerance;
    }
}
