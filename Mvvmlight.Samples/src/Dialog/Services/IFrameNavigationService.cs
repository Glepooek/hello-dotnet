using GalaSoft.MvvmLight.Views;

namespace Dialog.Services
{
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; set; }
        void GoBack(object parameter);
    }
}
