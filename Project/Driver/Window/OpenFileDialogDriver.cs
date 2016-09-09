using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using System.Linq;

namespace Driver.Window
{
    public class OpenFileDialogDriver
    {
        WindowControl _window;

        public NativeButton Button_開く { get; set; }
        public NativeButton Button_キャンセル { get; set; }
        public NativeComboBox ComboBox_FilePath { get; private set; }

        public OpenFileDialogDriver(WindowControl window)
        {
            _window = window;
            var combo = _window.GetFromWindowClass("ComboBoxEx32").OrderBy(e => NativeMethods.GetTop(e)).Last();
            ComboBox_FilePath = new NativeComboBox(combo);
            Button_開く = new NativeButton(_window.IdentifyFromWindowText("開く(&O)"));
            Button_キャンセル = new NativeButton(_window.IdentifyFromWindowText("キャンセル"));
        }
    }
}