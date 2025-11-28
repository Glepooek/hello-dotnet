using Stylet.Samples.NavigationController.Pages;
using StyletIoC;
using System;

namespace Stylet.Samples.NavigationController
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            builder.Bind<NavigationController>().And<INavigationController>().To<NavigationController>().InSingletonScope();
            builder.Bind<Func<Page1ViewModel>>().ToFactory<Func<Page1ViewModel>>(c => () => c.Get<Page1ViewModel>());
            builder.Bind<Func<Page2ViewModel>>().ToFactory<Func<Page2ViewModel>>(c => () => c.Get<Page2ViewModel>());
        }

        protected override void OnLaunch()
        {
            var navigationController = this.Container.Get<NavigationController>();
            navigationController.Delegate = this.RootViewModel;
            navigationController.NavigateToPage1();
        }
    }
}
