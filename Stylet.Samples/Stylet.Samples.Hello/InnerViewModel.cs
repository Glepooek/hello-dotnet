namespace Stylet.Samples.Hello
{
    public class InnerViewModel
    {
        private IWindowManager mWindowManager;

        public InnerViewModel(IWindowManager windowManager)
        {
            this.mWindowManager = windowManager;
        }

        public void DoSomething()
        {
            mWindowManager.ShowMessageBox("invoked in InnerViewModel");
        }
    }
}
