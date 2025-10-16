using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace Test.Multilanguage
{
    /// <summary>
    /// 全局类
    /// </summary>
    public static class GlobalConfig
    {
        #region Fields
        private static Resource mStringResource;
        /// <summary>
        /// 语言改变通知事件
        /// </summary>
        public static EventHandler<EventArgs> LanguageChangedEvent;
        #endregion

        #region Properties
        static bool? isDesignMode = null;
        /// <summary>
        /// 判断是设计模式还是运行模式
        /// </summary>
        public static bool IsDesignMode
        {
            get
            {
                if (isDesignMode == null)
                {
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    isDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
                }

                return isDesignMode.GetValueOrDefault(false);
            }
        }
        #endregion

        #region Constructor
        static GlobalConfig()
        {
            mStringResource = new Resource();
        }
        #endregion

        #region Methods
        /// <summary>
        /// 获取资源内容
        /// </summary>
        /// <param name="key">资源Key</param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            return mStringResource.GetResourceString(key);
        }

        /// <summary>
        /// 改变语言
        /// </summary>
        /// <param name="language">区域性名称</param>
        public static void ChangeLanguage(string language)
        {
            if (mStringResource.CurrentCulture.Name.Equals(language))
            {
                return;
            }

            CultureInfo culture = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            mStringResource.CurrentCulture = culture;

            LanguageChangedEvent?.Invoke(null, null);
        }
        #endregion
    }
}
