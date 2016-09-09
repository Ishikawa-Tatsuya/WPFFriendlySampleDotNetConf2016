using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace Driver.Window
{
    public class MessageBoxDriver
    {
        public NativeMessageBox Core { get; }

        public MessageBoxDriver(WindowControl core)
        {
            Core = new NativeMessageBox(core);
        }

        public void Button_OK_Click()
            => Core.EmulateButtonClick("OK");
        public void Button_はい_Click()
            => Core.EmulateButtonClick("はい(&Y)");
        public void Button_いいえ_Click()
            => Core.EmulateButtonClick("いいえ(&N)");
    }
}
