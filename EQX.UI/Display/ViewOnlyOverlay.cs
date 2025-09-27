using System.Runtime.InteropServices;

namespace EQX.UI.Display
{
    public class ViewOnlyOverlay
    {
        private ViewOnlyOverlayWindow? _overlay;

        public void ShowOn(int monitorIndex)
        {
            if (_overlay != null)
                return;

            var monitors = EnumerateMonitors();
            if (monitorIndex < 0 || monitorIndex >= monitors.Count)
                return;

            var target = monitors[monitorIndex];
            _overlay = new ViewOnlyOverlayWindow
            {
                Left = target.Left,
                Top = target.Top,
                Width = target.Width,
                Height = target.Height
            };
            _overlay.Show();
        }

        public void Hide()
        {
            _overlay?.Close();
            _overlay = null;
        }

        private static IList<MonitorInfo> EnumerateMonitors()
        {
            var result = new List<MonitorInfo>();
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                (IntPtr hMonitor, IntPtr hdc, ref RECT rect, IntPtr data) =>
                {
                    result.Add(new MonitorInfo(rect.Left,
                                               rect.Top,
                                               rect.Right - rect.Left,
                                               rect.Bottom - rect.Top));
                    return true;
                }, IntPtr.Zero);
            return result;
        }

        private record MonitorInfo(int Left, int Top, int Width, int Height);

        private delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdc, ref RECT lprcMonitor, IntPtr dwData);

        [DllImport("user32.dll")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}
