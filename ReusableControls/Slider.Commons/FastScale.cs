using System.Windows;

namespace Digihail.Controls
{
    /// <summary>
    /// 刻度单元
    /// </summary>
    public class FastScale
    {
        /// <summary>
        /// 刻度范围
        /// </summary>
        public double Size
        {
            get;
            set;
        }

        /// <summary>
        /// 刻度标识
        /// </summary>
        public string Label
        {
            get;
            set;
        }

        /// <summary>
        /// 刻度横向对齐方式，影响刻度位置
        /// </summary>
        public HorizontalAlignment LabelHorizontalAlignment
        {
            get;
            set;
        }

        /// <summary>
        ///  容器横向对齐方式，影响刻度位置
        /// </summary>
        public HorizontalAlignment ContainerHorizontalAlignment
        {
            get;
            set;
        }

        /// <summary>
        /// 刻度竖向对齐方式，影响刻度位置
        /// </summary>
        public VerticalAlignment LabelVerticalAlignment
        {
            get;
            set;
        }

        /// <summary>
        /// 容器竖向对齐方式，影响刻度位置
        /// </summary>
        public VerticalAlignment ContainerVerticalAlignment
        {
            get;
            set;
        }

        /// <summary>
        /// 边距
        /// </summary>
        public Thickness Margin { get; set; }
    }
}
