using CefSharp.Wpf;
using CefSharp.Wpf.Experimental;
using WebInWpf.Cefsharp.Handlers;

namespace WebInWpf.Cefsharp.Controls;

internal class UniStuChromiumBrowser : ChromiumWebBrowser
{
    #region Constructor

    public UniStuChromiumBrowser() : base()
    {
        this.LifeSpanHandler = new CefLifeSpanHandler();
        this.DownloadHandler = new CefDownloadHandler();
        this.MenuHandler = new CefMenuHandler();
        this.WpfKeyboardHandler = new WpfImeKeyboardHandler(this);
    }

    #endregion
}