using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using System;
using System.Runtime.InteropServices;

namespace Driver.Window
{
    class NativeMethods
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        internal static int GetTop(WindowControl w)
        {
            RECT rc;
            GetWindowRect(w.Handle, out rc);
            return rc.Top;
        }
    }
}
