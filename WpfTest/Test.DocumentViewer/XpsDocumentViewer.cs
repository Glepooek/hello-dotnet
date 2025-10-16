using System.Windows;
using System.Windows.Documents;

namespace Test.DocumentViewer
{
    /// <summary>
    /// 自定义DocumentViewer控件
    /// </summary>
    public class XpsDocumentViewer : System.Windows.Controls.DocumentViewer
    {
        #region Constructor

        static XpsDocumentViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XpsDocumentViewer), new FrameworkPropertyMetadata(typeof(XpsDocumentViewer)));
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Document代理
        /// </summary>
        public IDocumentPaginatorSource DocumentProxy
        {
            get { return (IDocumentPaginatorSource)GetValue(DocumentProxyProperty); }
            set { SetValue(DocumentProxyProperty, value); }
        }

        /// <summary>
        /// Document代理依赖项属性
        /// </summary>
        public static readonly DependencyProperty DocumentProxyProperty =
            DependencyProperty.Register(nameof(DocumentProxy), typeof(IDocumentPaginatorSource), typeof(XpsDocumentViewer), new PropertyMetadata(null, OnDocumentProxyChanged));

        private static void OnDocumentProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is XpsDocumentViewer viewer && e.NewValue != null)
            {
                viewer.Document = e.NewValue as IDocumentPaginatorSource;
                viewer.Zoom = 100;
                viewer.FitToWidth();
            }
        }

        #endregion

        #region Override Methods

        protected override void OnDecreaseZoomCommand()
        {

        }

        protected override void OnIncreaseZoomCommand()
        {

        }

        #endregion
    }
}
