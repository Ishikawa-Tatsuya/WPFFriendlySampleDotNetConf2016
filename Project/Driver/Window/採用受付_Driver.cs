﻿using Codeer.Friendly;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;

namespace Driver.Window
{
    public class 採用受付_Driver
    {
        AppVar _core;
        WPFTabControl Tab => new WPFTabControl(_core.LogicalTree().ByType<TabControl>().Single());
        WPFMenuBase Menu => new WPFMenuBase(_core.LogicalTree().ByType<Menu>().Single());

        public 採用受付_Driver(AppVar core)
        {
            _core = core;
        }

        public 登録_Driver Select_登録()
        {
            Tab.EmulateChangeSelectedIndex(0);
            return new 登録_Driver(Tab.VisualTree().ByType("WpfApplication.EntryControl").Single());
        }

        public 一覧_Driver Select_一覧()
        {
            Tab.EmulateChangeSelectedIndex(1);
            return new 一覧_Driver(Tab.VisualTree().ByType("WpfApplication.ViewControl").Single());
        }

        public Version_Driver Menu_ヘルプ_バージョン_Click()
        {
            Menu.GetItem("ヘルプ", "バージョン").EmulateClick(new Async());
            return new Version_Driver(WindowControl.WaitForIdentifyFromTypeFullName(Menu.App, "WpfApplication.VersionWindow"));
        }

        public OpenFileDialogDriver Menu_ファイル_開く_Click()
        {
            Menu.GetItem("ファイル", "開く").EmulateClick(new Async());
            return new OpenFileDialogDriver(WindowControl.WaitForIdentifyFromWindowText(Menu.App, "開く"));
        }

        public SaveFileDialogDriver Menu_ファイル_保存_Click()
        {
            Menu.GetItem("ファイル", "保存").EmulateClick(new Async());
            return new SaveFileDialogDriver(WindowControl.WaitForIdentifyFromWindowText(Menu.App, "名前を付けて保存"));
        }
    }
}
