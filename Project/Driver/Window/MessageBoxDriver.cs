using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace Driver.Window
{
    public class MessageBoxDriver
    {
        NativeMessageBox _core;

        public string Message => _core.Message;

        public MessageBoxDriver(WindowControl core)
        {
            _core = new NativeMessageBox(core);
        }

        public void Button_OK_Click()
            => _core.EmulateButtonClick("OK");
        public void Button_はい_Click()
            => _core.EmulateButtonClick("はい(&Y)");
        public void Button_いいえ_Click()
            => _core.EmulateButtonClick("いいえ(&N)");
    }
}
