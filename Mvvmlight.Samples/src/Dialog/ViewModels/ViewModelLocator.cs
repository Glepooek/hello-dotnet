using CommonServiceLocator;
using Dialog.Services;
using GalaSoft.MvvmLight.Ioc;
using MvvmDialogs;
using System;

namespace Dialog.ViewModels
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(() => CreateNavigationService());
            SimpleIoc.Default.Register<IDialogService>(() => new DialogService());
            //SimpleIoc.Default.Register<IDialogService>(() => new DialogService(frameworkDialogFactory: new CustomFrameworkDialogFactory()));

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PageOneViewModel>();
            SimpleIoc.Default.Register<PageTwoViewModel>();
            SimpleIoc.Default.Register<PageThreeViewModel>();
        }

        private static IFrameNavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(PageKeyConstant.PageOne, new Uri("/Dialog;component/Views/PageOne.xaml", UriKind.Relative));
            navigationService.Configure(PageKeyConstant.PageTwo, new Uri("/Dialog;component/Views/PageTwo.xaml", UriKind.Relative));
            navigationService.Configure(PageKeyConstant.PageThree, new Uri("/Dialog;component/Views/PageThree.xaml", UriKind.Relative));
            return navigationService;
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public PageOneViewModel PageOne
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PageOneViewModel>();
            }
        }

        public PageTwoViewModel PageTwo
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PageTwoViewModel>();
            }
        }

        public PageThreeViewModel PageThree
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PageThreeViewModel>();
            }
        }

        public static void Cleanup()
        {
        }
    }
}
