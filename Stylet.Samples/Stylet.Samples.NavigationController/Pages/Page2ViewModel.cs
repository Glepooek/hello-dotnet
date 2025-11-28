using System;

namespace Stylet.Samples.NavigationController.Pages
{
    public class Page2ViewModel : Screen
    {
        private readonly INavigationController navigationController;

        private string mInitiator;
        public string Initiator
        {
            get { return mInitiator; }
            set { SetAndNotify(ref mInitiator, value); }
        }


        public Page2ViewModel(INavigationController navigationController)
        {
            this.navigationController = navigationController ?? throw new ArgumentException(nameof(navigationController));
        }

        public void NavigateToPage1() => navigationController.NavigateToPage1();
    }
}
