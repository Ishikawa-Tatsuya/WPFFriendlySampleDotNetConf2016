using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using RM.Friendly.WPFStandardControls;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Driver.Window
{
    public class 登録_Driver
    {
        AppVar _core;

        public WPFTextBox TextBox_名前 => new WPFTextBox(_core.LogicalTree().ByBinding("Name.Value").Single());
        public WPFTextBox TextBox_メールアドレス => new WPFTextBox(_core.LogicalTree().ByBinding("Mail.Value").Single());
        public WPFComboBox CombBox_得意な言語 => new WPFComboBox(_core.LogicalTree().ByBinding("Language.Value").Single());
        public WPFToggleButton CheckBox_男性 => new WPFToggleButton(_core.LogicalTree().ByBinding("IsMan.Value").Single());
        public WPFToggleButton CheckBox_女性 => new WPFToggleButton(_core.LogicalTree().ByBinding("IsMan.Value").Single());
        public WPFCalendar Calendar_生年月日 => new WPFCalendar(_core.LogicalTree().ByBinding("BirthDay.Value").Single());
        WPFButtonBase Button_登録 => new WPFButtonBase(_core.LogicalTree().ByType<Button>().Single());

        public 登録_Driver(AppVar core)
        {
            _core = core;
        }

        public NativeMessageBox Button_登録_Click()
        {
            var current = WindowControl.FromZTop(Button_登録.App);
            var async = new Async();
            Button_登録.EmulateClick(async);
            var msg = current.WaitForNextModal(async);
            return msg == null ? null : new NativeMessageBox(msg);
        }
    }
}
