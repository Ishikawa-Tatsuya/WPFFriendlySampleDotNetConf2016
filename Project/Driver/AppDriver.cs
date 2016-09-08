using Codeer.Friendly.Windows;
using System;
using System.Diagnostics;
using System.IO;

namespace Driver
{
    public class AppDriver : IDisposable
    {
        public Process Process { get; private set; }
        WindowsAppFriend _app;

        public AppDriver()
        {
            if (Process != null)
            {
                return;
            }
            var dir = Path.GetFullPath("../../../WpfApplication/bin/Release");
            var pathExe = dir + "/WpfApplication.exe";
            var info = new ProcessStartInfo(pathExe) { WorkingDirectory = dir };
            Process = Process.Start(pathExe);
            _app = new WindowsAppFriend(Process);
        }

        public void Dispose()
        {
            _app.Dispose();
            try
            {
                Process.Kill();
            }
            catch { }
            Process = null;
        }
    }
}
