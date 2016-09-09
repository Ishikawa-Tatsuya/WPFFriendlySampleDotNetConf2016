using Microsoft.VisualStudio.TestTools.UnitTesting;
using Driver;
using System;

namespace Scenario
{
    [TestClass]
    public class UnitTest1
    {
        AppDriver _app;

        [TestInitialize]
        public void TestInitialize()
        {
            _app = new AppDriver();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _app.Dispose();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var entry = _app.採用受付.Select_登録();
            entry.TextBox_名前.EmulateChangeText("石川");
            entry.TextBox_メールアドレス.EmulateChangeText("xxx@bbb");
            entry.CombBox_得意な言語.EmulateChangeSelectedIndex(1);
            entry.CheckBox_男性.EmulateCheck(true);
            entry.Calendar_生年月日.EmulateChangeDate(new DateTime(1977, 1, 7));
            entry.Button_登録_Click();

            var view = _app.採用受付.Select_一覧();
            view.Button_検索.EmulateClick();
            view.Button_削除.EmulateClick();
            Assert.AreEqual(1, view.DataGrid.RowCount);
        }
    }
}
