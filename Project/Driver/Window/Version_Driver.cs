using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;

namespace Driver.Window
{
    public class Version_Driver
    {
        WindowControl _core;
        public WPFButtonBase Button_閉じる => new WPFButtonBase(_core.LogicalTree().ByType<Button>().Single());
        public Version_Driver(WindowControl core)
        {
            _core = core;
        }
    }
}
