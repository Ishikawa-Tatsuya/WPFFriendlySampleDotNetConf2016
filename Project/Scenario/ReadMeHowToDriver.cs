using Microsoft.VisualStudio.TestTools.UnitTesting;
using Driver;
using System;

namespace Scenario
{
    [TestClass]
    public class ReadMeHowToDriver
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
        public void Sample()
        {
            var entry = _app.採用受付.Select_登録();
            entry.TextBox_名前.EmulateChangeText("石川");
            entry.TextBox_メールアドレス.EmulateChangeText("xxx@bbb");
            entry.CombBox_得意な言語.EmulateChangeSelectedIndex(1);
            entry.CheckBox_男性.EmulateCheck(true);
            entry.Button_登録_Click().Button_OK_Click();
            entry.Calendar_生年月日.EmulateChangeDate(new DateTime(1977, 1, 7));
            entry.Button_登録_Click();

            var view = _app.採用受付.Select_一覧();
            view.Button_検索.EmulateClick();
            Assert.AreEqual(1, view.DataGrid.RowCount);
            view.DataGrid.GetRow(0).EmulateChangeSelected(true);
            view.Button_削除_Click().Button_いいえ_Click();
            view.Button_削除_Click().Button_はい_Click();
            Assert.AreEqual(0, view.DataGrid.RowCount);

            var version = _app.採用受付.Menu_ヘルプ_バージョン_Click();
            version.Button_閉じる.EmulateClick();

            var fileOpen = _app.採用受付.Menu_ファイル_開く_Click();
            fileOpen.ComboBox_FilePath.EmulateChangeEditText("xxx");
            fileOpen.Button_キャンセル.EmulateClick();

            var saveOpen = _app.採用受付.Menu_ファイル_保存_Click();
            saveOpen.ComboBox_FilePath.EmulateChangeEditText("xxx");
            saveOpen.Button_キャンセル.EmulateClick();
        }
    }
}
