using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using System.Linq;

namespace Driver.Window
{
    public class SaveFileDialogDriver
    {
        WindowControl _window;

        public NativeButton Button_保存 { get; set; }
        public NativeButton Button_キャンセル { get; set; }
        public NativeComboBox ComboBox_FilePath { get; private set; }

        public SaveFileDialogDriver(WindowControl window)
        {
            _window = window;
            var combo = _window.GetFromWindowClass("ComboBox").OrderBy(e => NativeMethods.GetTop(e)).ToArray()[1];
            ComboBox_FilePath = new NativeComboBox(combo);
            Button_保存 = new NativeButton(_window.IdentifyFromWindowText("保存(&S)"));
            Button_キャンセル = new NativeButton(_window.IdentifyFromWindowText("キャンセル"));
        }
    }
}
