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
        public void Friendlyの基本機能()
        {
            var main = _app.Type<Application>().Current.MainWindow;
            main.Title = "xxx";

            main.Background = _app.Type<Brushes>().Black;
            main.GlowBrush = _app.Type<Brushes>().Black;

            string result = main.Func(5);
            Assert.AreEqual("5", result);

            //データ書き換えたり
            var infos = main.DataContext._infos;
            infos.Clear();
            infos.AddRange(
                new[] {
                    new EntryInfo() { Name = "石川", Mail = "aaa@bbb.com", Language = "C#", IsMan = true, BirthDay = new DateTime(1977, 1, 7) }
                });

            //DLLインジェクション
            WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);

            //メソッド呼び出し
            _app.Type(GetType()).ChangeTabColor(main);

            //モックも利用できます。
            var mock = _app.Type<MockCommunicator>()();
            main.DataContext._communicator = mock;
            
            //一旦手動で通信処理を実行

            string data = mock.Data;
            Assert.AreEqual("石川", data);
        }

        static void ChangeTabColor(Window main)
        {
            FindTab(main).Background = Brushes.Red;
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
