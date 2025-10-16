using CefSharp;
using System;

namespace WebInWpf.Cefsharp.NET452.Controls.Handlers
{
    public class LoadHandler : ILoadHandler
    {
        private Action LoadStart = null;
        private Action<bool> LoadEnd = null;
        private Action LoadError = null;

        public LoadHandler(Action loadStart, Action<bool> loadEnd, Action loadError)
        {
            LoadStart = loadStart;
            LoadEnd = loadEnd;
            LoadError = loadError;
        }

        public void OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs)
        {
            if (LoadEnd == null)
            {
                return;
            }

            if (frameLoadEndArgs.Url.Equals("about:blank"))
            {
                LoadEnd.Invoke(false);
                return;
            }

            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + "  OnFrameLoadEnd  " + frameLoadEndArgs.Url);
            LoadEnd.Invoke(true);
        }

        public void OnFrameLoadStart(IWebBrowser chromiumWebBrowser, FrameLoadStartEventArgs frameLoadStartArgs)
        {
            if (LoadStart == null || frameLoadStartArgs.Url.Equals("about:blank"))
            {
                return;
            }

            LoadStart.Invoke();
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + "  OnFrameLoadStart  " + frameLoadStartArgs.Url);
        }

        public void OnLoadError(IWebBrowser chromiumWebBrowser, LoadErrorEventArgs loadErrorArgs)
        {
            if (LoadError == null)
            {
                return;
            }

            LoadError.Invoke();
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + "  OnLoadError  " + loadErrorArgs.ErrorCode + "  " + loadErrorArgs.ErrorText + "  " + loadErrorArgs.FailedUrl);
        }

        public void OnLoadingStateChange(IWebBrowser chromiumWebBrowser, LoadingStateChangedEventArgs loadingStateChangedArgs)
        {
        }
    }
}
