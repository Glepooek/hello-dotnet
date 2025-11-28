namespace Stylet.Samples.OverridingViewManager
{
    public class Page1ViewModel : Screen
    {
        private string mPageName;

        public string PageName
        {
            get { return mPageName; }
            set { SetAndNotify(ref mPageName, value); }
        }

        public Page1ViewModel()
        {
            PageName = "this is page1";
        }
    }
}
