using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.MusicStore.Messages;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Diagnostics;
using Xilium.CefGlue.Avalonia;

namespace Avalonia.MusicStore.Views;

public partial class WebViewWindow : Window, IRecipient<MessageParam>
{
    private AvaloniaCefBrowser cefBrowser;

    public WebViewWindow()
    {
        InitializeComponent();
        this.Loaded += WebViewWindow_Loaded;
        this.Unloaded += WebViewWindow_Unloaded;
        WeakReferenceMessenger.Default.Register<MessageParam>(this);
    }

    private void WebViewWindow_Unloaded(object? sender, Interactivity.RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Unregister<MessageParam>(this);
        cefBrowser?.Dispose();
    }

    private void WebViewWindow_Loaded(object? sender, Interactivity.RoutedEventArgs e)
    {
        cefBrowser = new AvaloniaCefBrowser();
        //cefBrowser.Address = "https://www.cnblogs.com";
        cefBrowser.Address = $"{AppDomain.CurrentDomain.BaseDirectory}TestWeb\\index.html";
        cefBrowser.LoadEnd += CefBrowser_LoadEnd;

        //this.Content = cefBrowser;
        panel.Children.Add(cefBrowser);
        cefBrowser.RegisterJavascriptObject(new JSCallback(this), "AvaWebView");
    }

    private void CefBrowser_LoadEnd(object sender, Xilium.CefGlue.Common.Events.LoadEndEventArgs e)
    {
        Dispatcher.UIThread.Invoke(new Action(() =>
        {
            panel.Children.Remove(canvas);
        }));
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            this.BeginMoveDrag(e);
        }
    }

    public void Receive(MessageParam message)
    {
        if (message == null)
        {
            return;
        }

        if (message.Reult)
        {
            //cefBrowser.ExecuteJavaScript("calculateAdd1(12,13)");
            cefBrowser.ExecuteJavaScript("app4.calculateAdd(12,13)");
        }
    }

    public class JSCallback
    {
        private Window webViewWindow;

        public JSCallback(Window window)
        {
            webViewWindow = window;
        }

        // 方法名小写开头
        public void closeWindow()
        {
            Debug.WriteLine("JS Call C#: closeWindow");
            Dispatcher.UIThread.Invoke(() =>
            {
                webViewWindow.Close();
            });
        }

        public void minimizeWindow()
        {
            Debug.WriteLine("JS Call C#: minimizeWindow");
            Dispatcher.UIThread.Invoke(() =>
            {
                webViewWindow.WindowState = WindowState.Minimized;
            });
        }

        public void mouseDownDrag()
        {
            Debug.WriteLine("JS Call C#: mouseDownDrag");
        }
    }
}