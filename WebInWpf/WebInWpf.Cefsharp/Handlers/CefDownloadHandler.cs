using CefSharp;
using System;

namespace WebInWpf.Cefsharp.Handlers;

internal class CefDownloadHandler : IDownloadHandler
{
    public event EventHandler<DownloadItem>? OnBeforeDownloadFired;

    public event EventHandler<DownloadItem>? OnDownloadUpdatedFired;

    public bool CanDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, string url, string requestMethod)
    {
        return true;
    }

    public bool OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
    {
        OnBeforeDownloadFired?.Invoke(this, downloadItem);

        if (!callback.IsDisposed)
        {
            using (callback)
            {
                callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
            }
            return true;
        }

        return false;
    }

    public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
    {
        OnDownloadUpdatedFired?.Invoke(this, downloadItem);
    }
}

