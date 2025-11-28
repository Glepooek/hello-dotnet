namespace Stylet.Samples.OverridingViewManager
{
    public class ShellViewModel : Conductor<IScreen>
    {
        public ShellViewModel(Page1ViewModel page1ViewModel)
        {
            this.DisplayName = "ShellView";
            this.ActiveItem = page1ViewModel;
        }
    }
}
