using Stylet;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;

namespace Test.DocumentViewer
{
    public class ShellViewModel : Screen
    {
        #region Costructor

        public ShellViewModel()
        {
            mAppBasePath = AppDomain.CurrentDomain.BaseDirectory;
            UserAgreementItems = new List<UserAgreementItem>()
            {
                new UserAgreementItem (){ Name="服务协议", Path= $"{mAppBasePath}Documents\\爱学习用户服务协议（20210318-终版-清洁版）.xps" },
                new UserAgreementItem(){ Name="隐私协议", Path=$"{mAppBasePath}Documents\\爱学习隐私权政策（20210318-终版-清洁版）.xps"}
            };
            SelectedUserAgreementItem = UserAgreementItems.FirstOrDefault();
        }

        #endregion

        #region Fields

        private string mAppBasePath = string.Empty;
        private UserAgreementItem mSelectedUserAgreementItem;

        #endregion

        #region Properties

        /// <summary>
        /// 用户协议集合
        /// </summary>
        public IEnumerable<UserAgreementItem> UserAgreementItems { get; set; }

        /// <summary>
        /// 被选中的用户协议
        /// </summary>
        public UserAgreementItem SelectedUserAgreementItem
        {
            get { return mSelectedUserAgreementItem; }
            set
            {
                SetAndNotify(ref mSelectedUserAgreementItem, value);
                LoadDocument();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 加载XPS文件
        /// </summary>
        public void LoadDocument()
        {
            if (File.Exists(SelectedUserAgreementItem.Path)
                && SelectedUserAgreementItem.Document == null)
            {
                XpsDocument xpsDocument = new XpsDocument(SelectedUserAgreementItem.Path, FileAccess.Read, CompressionOption.Fast);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    SelectedUserAgreementItem.Document = xpsDocument.GetFixedDocumentSequence();
                });
            }
        }

        #endregion
    }

    /// <summary>
    /// 用户协议
    /// </summary>
    public class UserAgreementItem
    {
        /// <summary>
        /// 协议名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 协议路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 协议文档
        /// </summary>
        public IDocumentPaginatorSource Document { get; set; }
    }
}
