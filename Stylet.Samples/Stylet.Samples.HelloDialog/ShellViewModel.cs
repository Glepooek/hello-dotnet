namespace Stylet.Samples.HelloDialog
{
    public class ShellViewModel : Screen
    {
        private readonly IWindowManager mWindowManager;
        private readonly IDialogFactory mDialogFactory;
        //private Func<DialogViewModel> mDialogFactory;

        private string mNameString;
        public string NameString
        {
            get { return mNameString; }
            set { SetAndNotify(ref mNameString, value); }
        }

        public ShellViewModel(IWindowManager windowManager, IDialogFactory dialogFactory)
        {
            this.DisplayName = "Hello Dialog";
            this.NameString = "click button to show dialog";

            this.mWindowManager = windowManager;
            this.mDialogFactory = dialogFactory;
        }

        //      public ShellViewModel(IWindowManager windowManager, Func<DialogViewModel> dialogFactory)
        //{
        //	this.DisplayName = "Hello Dialog";
        //	this.NameString = "click button to show dialog";

        //	this.mWindowManager = windowManager;
        //	this.mDialogFactory = dialogFactory;
        //}

        public void ShowDialog()
        {
            var dm = mDialogFactory.CreateDialog();
            //var dm = mDialogFactory();
            var result = mWindowManager.ShowDialog(dm);
            if (result.GetValueOrDefault())
            {
                this.NameString = $"your name is {dm.Name}";
            }
            else
            {
                this.NameString = $"Dialog is cancelled";
            }
        }
    }
}
