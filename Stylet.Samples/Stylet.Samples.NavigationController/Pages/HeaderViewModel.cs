using System;

namespace Stylet.Samples.NavigationController.Pages
{
    public class HeaderViewModel : Screen
    {
        private readonly INavigationController navigationController;

        public HeaderViewModel(INavigationController navigationController)
        {
            this.navigationController = navigationController ?? throw new ArgumentNullException(nameof(navigationController));
        }

        public void NavigateToPage1() => navigationController.NavigateToPage1();

        public void NavigateToPage2() => navigationController.NavigateToPage2("the Header");
    }
}
