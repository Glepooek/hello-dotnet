namespace FastScreenCapture.Models
{
    /// <summary>
    /// 工具类型枚举
    /// </summary>
    public enum Tool
    {
        /// <summary>
        /// 无状态可移动
        /// </summary>
        Null,
        /// <summary>
        /// 长方形
        /// </summary>
        Rectangle,
        /// <summary>
        /// 椭圆
        /// </summary>
        Ellipse,
        /// <summary>
        /// 画刷
        /// </summary>
        Line,
        /// <summary>
        /// 文字
        /// </summary>
        Text,
        /// <summary>
        /// 箭头
        /// </summary>
        Arrow,
        /// <summary>
        /// 撤销
        /// </summary>
        Revoke,
        /// <summary>
        /// 保存
        /// </summary>
        Save,
        /// <summary>
        /// 退出截图
        /// </summary>
        Cancel,
        /// <summary>
        /// 完成截图
        /// </summary>
        OK
    }
}
