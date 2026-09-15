using System.Runtime.InteropServices;

namespace FocusCube;

public static class NativeFeedback
{
    private const uint MbIconAsterisk = 0x00000040;

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool MessageBeep(uint type);

    public static void PlayCompletionSound()
    {
        _ = MessageBeep(MbIconAsterisk);
    }
}
