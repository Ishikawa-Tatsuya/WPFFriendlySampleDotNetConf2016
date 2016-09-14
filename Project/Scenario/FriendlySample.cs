using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Linq;
using WpfApplication;
using System;

namespace Scenario
{
    [TestClass]
    public class FriendlySample
    {
        WindowsAppFriend _app;
        
        [TestInitialize]
        public void TestInitialize()
        {
           // var pathExe = Path.GetFullPath("../../../WpfApplication/bin/Release/WpfApplication.exe");
            _app = new WindowsAppFriend(Process.GetProcessesByName("WpfApplication.vshost")[0]);
        }
        
        [TestCleanup]
        public void TestCleanup()
        {
            var id = _app.ProcessId;
            _app.Dispose();
         //   Process.GetProcessById(id).Kill();
        }

        [TestMethod]
        public void Test()
        {
            var main = _app.Type<Application>().Current.MainWindow;
            main.Title = "xxx";

            main.Background = _app.Type<Brushes>().Black;
            main.GlowBrush = _app.Type<Brushes>().Black;

            string result = main.Func(5);
            Assert.AreEqual("5", result);

            //DLLインジェクション
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);
            var tab = _app.Type(GetType()).FindTab(main);
            tab.Background = _app.Type<Brushes>().Red;

            //モックも利用できます。
            main.DataContext._communicator = _app.Type<MockCommunicator>()();

            //基本部分のデモなんで、ここでは手動で操作します。

            string data = main.DataContext._communicator.Data;
            Assert.AreEqual("石川", data);
        }

        static TabControl FindTab(FrameworkElement element)
        {
            foreach(var e in LogicalTreeHelper.GetChildren(element).Cast<object>().Where(e=>e is FrameworkElement).Cast<FrameworkElement>())
            {
                var tab = e as TabControl;
                if (tab != null) return tab;
                tab = FindTab(e);
                if (tab != null) return tab;
            }
            return null;
        }

        class MockCommunicator : ICommunicator
        {
            public string Data { get; set; }
            public bool SendData(string data)
            {
                Data = data;
                return true;
            }
        }
    }
}
