using CefSharp;

namespace WebInWpf.Cefsharp.NET452.Controls.Handlers
{
    public class DownloadHandler : IDownloadHandler
    {
        public bool CanDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, string url, string requestMethod)
        {
            return true;
        }

        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
                }
            }
        }

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            if (!downloadItem.IsComplete && !downloadItem.IsCancelled && downloadItem.IsInProgress)
            {
                // 有概率会重复调用,如暂停了再继续,所以h5要做好处理
                chromiumWebBrowser.GetMainFrame().ExecuteJavaScriptAsync($"downloadCallback('{downloadItem.OriginalUrl}',1)");
                return;
            }

            if (downloadItem.IsCancelled)
            {
                chromiumWebBrowser.GetMainFrame().ExecuteJavaScriptAsync($"downloadCallback('{downloadItem.OriginalUrl}',2)");
                return;
            }

            if (downloadItem.IsComplete)
            {
                chromiumWebBrowser.GetMainFrame().ExecuteJavaScriptAsync($"downloadCallback('{downloadItem.OriginalUrl}',0)");
            }
        }
    }
}
