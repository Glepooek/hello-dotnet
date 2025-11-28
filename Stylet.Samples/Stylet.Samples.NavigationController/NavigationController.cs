using Stylet.Samples.NavigationController.Pages;
using System;

namespace Stylet.Samples.NavigationController
{
    public class NavigationController : INavigationController
    {
        private readonly Func<Page1ViewModel> page1ViewModelFactory;
        private readonly Func<Page2ViewModel> page2ViewModelFactory;
        public INavigationControllerDelegate Delegate { get; set; }


        public NavigationController(Func<Page1ViewModel> page1ViewModelFactory, Func<Page2ViewModel> page2ViewModelFactory)
        {
            this.page1ViewModelFactory = page1ViewModelFactory ?? throw new ArgumentNullException(nameof(page1ViewModelFactory));
            this.page2ViewModelFactory = page2ViewModelFactory ?? throw new ArgumentNullException(nameof(page2ViewModelFactory));
        }


        public void NavigateToPage1()
        {
            this.Delegate?.Navigate(page1ViewModelFactory());
        }

        public void NavigateToPage2(string initiator)
        {
            //this.Delegate?.Navigate(page2ViewModelFactory());
            var vm = page2ViewModelFactory();
            vm.Initiator = initiator;
            this.Delegate?.Navigate(vm);
        }
    }
}
