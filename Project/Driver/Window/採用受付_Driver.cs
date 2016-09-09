using Codeer.Friendly;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;

namespace Driver.Window
{
    public class 採用受付_Driver
    {
        AppVar _core;
        WPFTabControl Tab => new WPFTabControl(_core.LogicalTree().ByType<TabControl>().Single());

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
    }
}
