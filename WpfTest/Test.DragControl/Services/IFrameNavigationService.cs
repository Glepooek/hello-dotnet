using GalaSoft.MvvmLight.Views;

namespace Test.DragControl.Services
{
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; set; }
        void GoBack(object parameter);
    }
}
