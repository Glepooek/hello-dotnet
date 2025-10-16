using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Test.DragControl.Services
{
    public class NavigationService : ViewModelBase, IFrameNavigationService
    {
        #region Constructor

        public NavigationService()
        {
            mPagesDic = new Dictionary<string, Uri>();
            mBackStack = new Stack<string>();
        }

        #endregion

        #region Fields

        private readonly Dictionary<string, Uri> mPagesDic;
        private readonly Stack<string> mBackStack;
        private string mCurrentPageKey;
        private bool mGoback = false;

        #endregion

        #region Properties

        public string CurrentPageKey
        {
            get
            {
                return mCurrentPageKey;
            }

            private set
            {
                Set(() => CurrentPageKey, ref mCurrentPageKey, value);
            }
        }

        public object Parameter { get; set; }

        #endregion

        #region Methods

        public void GoBack()
        {
            if (mBackStack.Count > 1)
            {
                mGoback = true;
                mBackStack.Pop();
                NavigateTo(mBackStack.Peek());
                mGoback = false;
            }
        }

        public void GoBack(object parameter)
        {
            if (mBackStack.Count > 1)
            {
                mGoback = true;
                mBackStack.Pop();
                NavigateTo(mBackStack.Peek(), parameter);
                mGoback = false;
            }
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            lock (mPagesDic)
            {
                if (!mPagesDic.ContainsKey(pageKey))
                {
                    throw new ArgumentException($"No such page: {pageKey}", "pageKey");
                }

                var frame = GetDescendantByName(Application.Current.MainWindow, "mainFrame") as Frame;

                if (frame != null)
                {
                    frame.Source = mPagesDic[pageKey];
                    if (!mGoback)
                    {
                        mBackStack.Push(pageKey);
                    }

                    Parameter = parameter;
                    CurrentPageKey = pageKey;
                }
            }
        }

        public void Configure(string key, Uri pageType)
        {
            lock (mPagesDic)
            {
                if (mPagesDic.ContainsKey(key))
                {
                    mPagesDic[key] = pageType;
                }
                else
                {
                    mPagesDic.Add(key, pageType);
                }
            }
        }

        private FrameworkElement GetDescendantByName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                var element = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (element != null)
                {
                    if (element.Name == name)
                    {
                        return element;
                    }

                    element = GetDescendantByName(element, name);
                    if (element != null)
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
