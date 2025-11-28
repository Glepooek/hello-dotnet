using GalaSoft.MvvmLight.Views;

namespace Navigation.Services
{
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; set; }
        void GoBack(object parameter);
    }
}
