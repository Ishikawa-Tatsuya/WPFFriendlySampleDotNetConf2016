using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System;
using System.Diagnostics;
using System.IO;
using Driver.Window;
using System.Windows;
using RM.Friendly.WPFStandardControls;

namespace Driver
{
    public class AppDriver : IDisposable
    {
        public Process Process { get; private set; }
        WindowsAppFriend _app;

        public 採用受付_Driver 採用受付 => new 採用受付_Driver(_app.Type<Application>().Current.MainWindow);
       
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
            WPFStandardControls_4.Injection(_app);
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
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
