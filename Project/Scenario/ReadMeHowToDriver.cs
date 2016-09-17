using Microsoft.VisualStudio.TestTools.UnitTesting;
using Driver;
using System;
using System.Collections.Generic;
using WpfApplication;
using System.Linq;

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
        public void ドライバ利用サンプル()
        {
            //登録
            var entry = _app.採用受付.Select_登録();
            entry.TextBox_名前.EmulateChangeText("石川");
            entry.TextBox_メールアドレス.EmulateChangeText("xxx@bbb");
            entry.CombBox_得意な言語.EmulateChangeSelectedIndex(1);
            entry.CheckBox_男性.EmulateCheck(true);

            //中途半端な入力をしたのでメッセージが出る
            var msg = entry.Button_登録_Click();
            //閉じる
            msg.Button_OK_Click();
            
            //誕生日を入力して
            entry.Calendar_生年月日.EmulateChangeDate(new DateTime(1977, 1, 7));

            //再び登録
            entry.Button_登録_Click();


            //一覧画面を開いて
            var view = _app.採用受付.Select_一覧();
            //行は一行である
            Assert.AreEqual(1, view.DataGrid.RowCount);

            //データを変更
            view.DataGrid.EmulateChangeCellText(0, 0, "xxx");
            view.DataGrid.EmulateChangeCellText(0, 1, "yyy");
            view.DataGrid.EmulateChangeCellComboSelect(0, 2, 3);
            view.DataGrid.EmulateChangeCellComboSelect(0, 3, 1);
            view.DataGrid.EmulateChangeDate(0, 4, new DateTime(1988, 8, 8));

            //一行目を選択して
            view.DataGrid.EmulateChangeCurrentCell(0, 0);

            //削除をクリック
            msg = view.Button_削除_Click();

            //確認メッセージで「はい」を選択
            msg.Button_はい_Click();

            //行数は0になる
            Assert.AreEqual(0, view.DataGrid.RowCount);

            //その他画面も開いてみる
            var version = _app.採用受付.Menu_ヘルプ_バージョン_Click();
            version.Button_閉じる.EmulateClick();

            var fileOpen = _app.採用受付.Menu_ファイル_開く_Click();
            fileOpen.ComboBox_FilePath.EmulateChangeEditText("xxx");
            fileOpen.Button_キャンセル.EmulateClick();

            var saveOpen = _app.採用受付.Menu_ファイル_保存_Click();
            saveOpen.ComboBox_FilePath.EmulateChangeEditText("xxx");
            saveOpen.Button_キャンセル.EmulateClick();
        }

        static readonly EntryInfo[] TestData = new[]
        {
            new EntryInfo(){Name = "大平 夏希", Mail = "oohira_natsuki@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1969/1/8")},
            new EntryInfo(){Name = "横田 れいな", Mail = "yokota_reina@example.com", Language = "Java", IsMan = false, BirthDay = DateTime.Parse("1960/8/17")},
            new EntryInfo(){Name = "三浦 正義", Mail = "miura_masayoshi@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1990/9/9")},
            new EntryInfo(){Name = "玉木 浩正", Mail = "tamaki_hiromasa@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1945/12/23")},
            new EntryInfo(){Name = "早坂 さんま", Mail = "hayasaka_sanma@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1946/8/12")},
            new EntryInfo(){Name = "高谷 文世", Mail = "takaya_fumiyo@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1973/11/27")},
            new EntryInfo(){Name = "奥野 誠治", Mail = "okuno_seiji@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1960/1/31")},
            new EntryInfo(){Name = "神保 花緑", Mail = "jinbo_karoku@example.com", Language = "C++", IsMan = true, BirthDay = DateTime.Parse("1972/1/17")},
            new EntryInfo(){Name = "千葉 直人", Mail = "chiba_naoto@example.com", Language = "C++", IsMan = true, BirthDay = DateTime.Parse("1977/4/1")},
            new EntryInfo(){Name = "荻野 小雁", Mail = "ogino_kogan@example.com", Language = "C++", IsMan = true, BirthDay = DateTime.Parse("1974/6/24")},
            new EntryInfo(){Name = "篠原 雅彦", Mail = "shinohara_masahiko@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1942/7/8")},
            new EntryInfo(){Name = "金丸 あさみ", Mail = "kanamaru_asami@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1957/9/10")},
            new EntryInfo(){Name = "落合 拓郎", Mail = "ochiai_takurou@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1948/9/21")},
            new EntryInfo(){Name = "有村 咲", Mail = "arimura_saki@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1982/1/20")},
            new EntryInfo(){Name = "谷川 恵梨香", Mail = "tanikawa_erika@example.com", Language = "Ruby", IsMan = false, BirthDay = DateTime.Parse("1983/11/21")},
            new EntryInfo(){Name = "益岡 ケンイチ", Mail = "masuoka_kenichi@example.com", Language = "JavaScript", IsMan = true, BirthDay = DateTime.Parse("1955/1/23")},
            new EntryInfo(){Name = "横川 丈雄", Mail = "yokokawa_takeo@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1950/12/17")},
            new EntryInfo(){Name = "明石家 竜也", Mail = "akashiya_tatsuya@example.com", Language = "Ruby", IsMan = true, BirthDay = DateTime.Parse("1979/12/14")},
            new EntryInfo(){Name = "朝倉 れいな", Mail = "asakura_reina@example.com", Language = "JavaScript", IsMan = false, BirthDay = DateTime.Parse("1994/9/12")},
            new EntryInfo(){Name = "高島 沙耶", Mail = "takashima_saya@example.com", Language = "C++", IsMan = false, BirthDay = DateTime.Parse("1959/6/5")},
            new EntryInfo(){Name = "河本 剛基", Mail = "koumoto_yoshiki@example.com", Language = "C++", IsMan = true, BirthDay = DateTime.Parse("1996/3/23")},
            new EntryInfo(){Name = "美輪 聡", Mail = "miwa_satoshi@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1954/6/16")},
            new EntryInfo(){Name = "根本 まなみ", Mail = "nemoto_manami@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1983/8/5")},
            new EntryInfo(){Name = "羽田 さゆり", Mail = "hata_sayuri@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1988/8/1")},
            new EntryInfo(){Name = "大木 あき", Mail = "ooki_aki@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1976/8/1")},
            new EntryInfo(){Name = "筒井 詩織", Mail = "tsutsui_shiori@example.com", Language = "Ruby", IsMan = false, BirthDay = DateTime.Parse("1943/8/7")},
            new EntryInfo(){Name = "小川 瞳", Mail = "ogawa_hitomi@example.com", Language = "JavaScript", IsMan = false, BirthDay = DateTime.Parse("1946/3/27")},
            new EntryInfo(){Name = "畠中 利夫", Mail = "hatanaka_toshio@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1949/7/3")},
            new EntryInfo(){Name = "熊倉 優", Mail = "kumakura_yuu@example.com", Language = "C++", IsMan = false, BirthDay = DateTime.Parse("1986/5/30")},
            new EntryInfo(){Name = "今田 竜也", Mail = "imada_tatsuya@example.com", Language = "C++", IsMan = true, BirthDay = DateTime.Parse("1941/3/1")},
            new EntryInfo(){Name = "谷口 隼士", Mail = "taniguchi_shunji@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1944/11/15")},
            new EntryInfo(){Name = "浦田 長利", Mail = "urata_nagatoshi@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1941/4/20")},
            new EntryInfo(){Name = "古屋 真希", Mail = "furuya_maki@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1970/12/9")},
            new EntryInfo(){Name = "赤羽 将也", Mail = "akabane_masaya@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1938/3/13")},
            new EntryInfo(){Name = "中村 景子", Mail = "nakamura_keiko@example.com", Language = "Ruby", IsMan = false, BirthDay = DateTime.Parse("1981/11/14")},
            new EntryInfo(){Name = "矢口 徹", Mail = "yaguchi_tooru@example.com", Language = "JavaScript", IsMan = true, BirthDay = DateTime.Parse("1964/10/24")},
            new EntryInfo(){Name = "野村 照生", Mail = "nomura_teruo@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1939/7/3")},
            new EntryInfo(){Name = "坂本 光博", Mail = "sakamoto_mitsuhiro@example.com", Language = "JavaScript", IsMan = true, BirthDay = DateTime.Parse("1983/8/31")},
            new EntryInfo(){Name = "長谷 あさみ", Mail = "nagaya_asami@example.com", Language = "Ruby", IsMan = false, BirthDay = DateTime.Parse("1971/2/12")},
            new EntryInfo(){Name = "緒方 恵麻", Mail = "ogata_ema@example.com", Language = "C", IsMan = false, BirthDay = DateTime.Parse("1959/7/24")},
            new EntryInfo(){Name = "平沢 美智子", Mail = "hirasawa_michiko@example.com", Language = "C", IsMan = false, BirthDay = DateTime.Parse("1969/2/17")},
            new EntryInfo(){Name = "田尻 有海", Mail = "tajiri_ami@example.com", Language = "Ruby", IsMan = false, BirthDay = DateTime.Parse("1979/8/31")},
            new EntryInfo(){Name = "田口 真奈美", Mail = "taguchi_manami@example.com", Language = "JavaScript", IsMan = false, BirthDay = DateTime.Parse("1987/9/21")},
            new EntryInfo(){Name = "結城 南朋", Mail = "yuuki_nao@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1959/5/19")},
            new EntryInfo(){Name = "真田 彩", Mail = "sanada_aya@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1977/5/17")},
            new EntryInfo(){Name = "筧 さとみ", Mail = "kakei_satomi@example.com", Language = "C#", IsMan = false, BirthDay = DateTime.Parse("1981/1/18")},
            new EntryInfo(){Name = "植木 英嗣", Mail = "ueki_hidetsugu@example.com", Language = "C#", IsMan = true, BirthDay = DateTime.Parse("1951/10/14")},
            new EntryInfo(){Name = "北島 誠一", Mail = "kitajima_seiichi@example.com", Language = "JavaScript", IsMan = true, BirthDay = DateTime.Parse("1983/7/11")},
            new EntryInfo(){Name = "砂川 和之", Mail = "sunakawa_kazuyuki@example.com", Language = "JavaScript", IsMan = true, BirthDay = DateTime.Parse("1985/2/1")},
            new EntryInfo(){Name = "藤野 芽以", Mail = "fujino_mei@example.com", Language = "JavaScript", IsMan = false, BirthDay = DateTime.Parse("1978/5/22")},
        };
        static readonly List<string> Languages = new List<string>(new[] { "C", "C++", "C#", "Java", "JavaScript", "Ruby" });

        [TestMethod]
        public void 検索のテスト()
        {
            //データ登録
            var entry = _app.採用受付.Select_登録();
            foreach (var data in TestData)
            {
                entry.TextBox_名前.EmulateChangeText(data.Name);
                entry.TextBox_メールアドレス.EmulateChangeText(data.Mail);
                entry.CombBox_得意な言語.EmulateChangeSelectedIndex(Languages.IndexOf(data.Language));
                if (data.IsMan) entry.CheckBox_男性.EmulateCheck(true);
                else entry.CheckBox_女性.EmulateCheck(true);
                entry.Calendar_生年月日.EmulateChangeDate(data.BirthDay);
                entry.Button_登録_Click();
            }

            var view = _app.採用受付.Select_一覧();

            view.CombBox_得意言語.EmulateChangeSelectedIndex(Languages.IndexOf("JavaScript") + 1);
            view.Button_検索.EmulateClick();

            int count = view.DataGrid.RowCount;
            Assert.AreEqual(9, count);
            var row = 0;
            Assert.AreEqual("益岡 ケンイチ", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("朝倉 れいな", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("小川 瞳", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("矢口 徹", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("坂本 光博", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("田口 真奈美", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("北島 誠一", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("砂川 和之", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("藤野 芽以", view.DataGrid.GetCellText(row++, 0));
        }
        
        [TestMethod]
        public void 登録を高速化()
        {
            //データ挿入
            _app.採用受付.データ挿入(TestData);

            var view = _app.採用受付.Select_一覧();

            view.CombBox_得意言語.EmulateChangeSelectedIndex(Languages.IndexOf("JavaScript") + 1);
            view.Button_検索.EmulateClick();

            int count = view.DataGrid.RowCount;
            Assert.AreEqual(9, count);
            var row = 0;
            Assert.AreEqual("益岡 ケンイチ", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("朝倉 れいな", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("小川 瞳", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("矢口 徹", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("坂本 光博", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("田口 真奈美", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("北島 誠一", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("砂川 和之", view.DataGrid.GetCellText(row++, 0));
            Assert.AreEqual("藤野 芽以", view.DataGrid.GetCellText(row++, 0));
        }

        [TestMethod]
        public void 通信テスト()
        {
            _app.採用受付.データ挿入(TestData);
            var receiver = _app.採用受付.通信モック利用();

            var msg = _app.採用受付.Menu_送信_Click();
            Assert.AreEqual("Success", msg.Message);
            msg.Button_OK_Click();

            Assert.AreEqual(string.Join(Environment.NewLine, TestData.Select(e => e.Name)), receiver.Data);
        }
    }
}
