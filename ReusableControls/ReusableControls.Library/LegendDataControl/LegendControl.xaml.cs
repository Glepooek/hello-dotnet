using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Digihail.Controls
{
    /// <summary>
    /// LegendControl.xaml 的交互逻辑
    /// </summary>
    public partial class LegendControl : UserControl
    {

        #region DependencyProperty

        /// <summary>
        /// 图形类型
        /// </summary>
        public GraphicsType Type
        {
            get { return (GraphicsType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(GraphicsType), typeof(LegendControl), new PropertyMetadata(GraphicsType.Circular));

        /// <summary>
        /// 是否显示数值
        /// </summary>
        public bool IsVisibleNumber
        {
            get { return (bool)GetValue(IsVisibleNumberProperty); }
            set { SetValue(IsVisibleNumberProperty, value); }
        }
        public static readonly DependencyProperty IsVisibleNumberProperty =
            DependencyProperty.Register("IsVisibleNumber", typeof(bool), typeof(LegendControl), new PropertyMetadata(true));

        /// <summary>
        /// 排布方向
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(LegendControl), new PropertyMetadata(Orientation.Horizontal));

        /// <summary>
        /// 数据源
        /// </summary>
        public ObservableCollection<Legend> DataSource
        {
            get { return (ObservableCollection<Legend>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(ObservableCollection<Legend>), typeof(LegendControl), new PropertyMetadata(new ObservableCollection<Legend>()));

        #endregion

        public LegendControl()
        {
            InitializeComponent();
        }

        public enum GraphicsType
        {
            Circular,
            Square,
            Triangle
        }
    }
}
