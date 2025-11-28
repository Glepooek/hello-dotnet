namespace Stylet.Samples.NavigationController.Pages
{
    public class ShellViewModel : Conductor<IScreen>, INavigationControllerDelegate
    {
        public HeaderViewModel HeaderViewModel { get; }
        public ShellViewModel(HeaderViewModel headerViewModel)
        {
            this.DisplayName = "ShellView";
            this.HeaderViewModel = headerViewModel;
        }


        public void Navigate(IScreen screen)
        {
            ActivateItem(screen);
        }
    }
}
