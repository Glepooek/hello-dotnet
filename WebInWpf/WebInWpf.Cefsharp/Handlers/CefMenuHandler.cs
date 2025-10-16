using CefSharp;

namespace WebInWpf.Cefsharp.Handlers;

internal class CefMenuHandler : IContextMenuHandler
{
    private const int ShowDevTools = 26501;
    private const int CloseDevTools = 26502;

    public void OnBeforeContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
    {

    }

    public bool OnContextMenuCommand(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
    {
        if ((int)commandId == ShowDevTools)
        {
            browser.ShowDevTools();
        }
        if ((int)commandId == CloseDevTools)
        {
            browser.CloseDevTools();
        }
        return false;
    }

    public void OnContextMenuDismissed(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
    {

    }

    public bool RunContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
    {
        return true;// Return true to hide the ContextMenu.
    }
}

