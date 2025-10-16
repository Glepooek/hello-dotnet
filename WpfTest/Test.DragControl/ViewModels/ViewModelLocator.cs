using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using Test.DragControl.Services;
using Test.DragControl.Utils;

namespace Test.DragControl.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ListPageViewModel>();
            SimpleIoc.Default.Register<EditPageViewModel>();
            SimpleIoc.Default.Register(() => InitNavigationService());
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ListPageViewModel ListPageVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ListPageViewModel>();
            }
        }

        public EditPageViewModel EditPageVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditPageViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        private IFrameNavigationService InitNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(PageKeyConstant.LIST_PAGE_KEY, new Uri("/Test.DragControl;component/Views/ListPageView.xaml", UriKind.Relative));
            navigationService.Configure(PageKeyConstant.EDIT_PAGE_KEY, new Uri("/Test.DragControl;component/Views/EditPageView.xaml", UriKind.Relative));
            return navigationService;
        }
    }
}