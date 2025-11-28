namespace Stylet.Samples.TabNavigation
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public ShellViewModel(Page1ViewModel page1ViewModel, Page2ViewModel page2ViewModel)
        {
            this.DisplayName = "Shelle view";

            this.Items.Add(page1ViewModel);
            this.Items.Add(page2ViewModel);

            this.ActiveItem = page1ViewModel;
        }

    }
}
