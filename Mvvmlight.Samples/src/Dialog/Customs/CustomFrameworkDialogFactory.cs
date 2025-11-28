using MvvmDialogs.FrameworkDialogs;
using MvvmDialogs.FrameworkDialogs.MessageBox;

namespace Dialog.Customs
{
    public class CustomFrameworkDialogFactory : DefaultFrameworkDialogFactory
    {
        public override IMessageBox CreateMessageBox(MessageBoxSettings settings)
        {
            return new CustomMessageBox(settings);
        }
    }
}
