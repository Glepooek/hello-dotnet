using CefSharp;
using CefSharp.Wpf;

namespace WebInWpf.Cefsharp.Handlers;

internal class CefLifeSpanHandler : ILifeSpanHandler
{
    public bool DoClose(IWebBrowser browserControl, IBrowser browser)
    {
        return false;
    }

    public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
    {

    }

    public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
    {

    }

    public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame,
                            string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition,
                            bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo,
                            IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser? newBrowser)
    {
        newBrowser = null;
        var chromiumWebBrowser = browserControl as ChromiumWebBrowser;
        chromiumWebBrowser?.Load(targetUrl);

        return true; // Return true to cancel the popup creation.
    }
}

